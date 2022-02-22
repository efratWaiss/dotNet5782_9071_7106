using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    partial class BL : IBL
    {
        public BO.Customer GetCustomer(int id)
        {
            try
            {
                var customer = dal.GetCustomer(id);
                BO.Customer c = new BO.Customer(customer.Id, customer.Name, customer.Phone, new Location(customer.Longitude, customer.Latitude));
                foreach (var item in dal.GetListParcel())
                {
                    if (item.SenderId == c.Id)
                        c.parcelToCustomer.Add(new BO.Parcel(item.Id, new CustomerInParcel(c.Id, c.Name)
                            , new CustomerInParcel(dal.GetCustomer(item.TargetId).Id, dal.GetCustomer(item.TargetId).Name)
                            , (BO.WeightCategories)item.Weight
                            , (BO.Priorities)item.priority
                            , new DroneInParcel(DronesList.FirstOrDefault(x => x.Id == item.DroneId).Id, DronesList.FirstOrDefault(x => x.Id == item.DroneId).Battery, DronesList.FirstOrDefault(x => x.Id == item.DroneId).LocationNow)
                            , item.Requested
                            , DronesList.FirstOrDefault(x => x.Id == item.DroneId).Id
                            , item.scheduled
                            , item.PickedUp
                            , item.Delivered));
                    else if (item.TargetId == c.Id)
                    {
                        DroneToList drone = DronesList.FirstOrDefault(x => x.Id == item.DroneId);
                        if (drone != null)
                        {
                            c.parcelFromCustomer.Add(new BO.Parcel(item.Id, new CustomerInParcel(dal.GetCustomer(item.SenderId).Id, dal.GetCustomer(item.SenderId).Name)
                               , new CustomerInParcel(c.Id, c.Name)
                               , (BO.WeightCategories)item.Weight
                               , (BO.Priorities)item.priority
                               , new DroneInParcel(item.DroneId, drone.Battery, drone.LocationNow)
                               , item.Requested
                               , item.DroneId
                               , item.scheduled
                               , item.PickedUp
                               , item.Delivered));
                        }
                    }
                }
                return c;
            }
            catch (DO.NotExistException ex) { throw new BO.NotExistException(ex.Message); }

        }
        public BO.Drone GetDrone(int id)
        {

            ParcelInTransference ParcelInTransference;
            var drone = DronesList.FirstOrDefault(x => x.Id == id);

            if (drone != default)
            {
                var parcel = DronesList.FirstOrDefault(x => x.Id == drone.ParcelDelivered);
                if (drone.Status == DroneStatuses.Shipping)
                {
                    try
                    {
                        var p = dal.GetParcel(drone.ParcelDelivered);
                        ParcelInTransference = new(p.Id, new CustomerInParcel(p.SenderId, dal.GetCustomer(p.SenderId).Name), new CustomerInParcel(p.TargetId, dal.GetCustomer(p.TargetId).Name), (BO.WeightCategories)p.Weight, (BO.Priorities)p.priority, true,
                           new Location(dal.GetCustomer(p.SenderId).Longitude, dal.GetCustomer(p.SenderId).Latitude),
                           new Location(dal.GetCustomer(p.TargetId).Longitude, dal.GetCustomer(p.TargetId).Latitude)
                           , GetDistanceBetweenTwoLocation(new Location(dal.GetCustomer(p.SenderId).Longitude, dal.GetCustomer(p.SenderId).Latitude),
                           new Location(dal.GetCustomer(p.TargetId).Longitude, dal.GetCustomer(p.TargetId).Latitude)));
                    }
                    catch (DO.IdException ex)
                    {
                        throw new BO.IdException(ex.Message);
                    }
                }
                else
                {
                    ParcelInTransference = null;
                }
                BO.Drone d = new BO.Drone(drone.Id, drone.Model, drone.MaxWeight, drone.Status, drone.Battery, ParcelInTransference, drone.LocationNow);
                return d;
            }
            else
            {
                throw new BO.NotExistException("this id not exist in the system");
            }

        }
        public BO.Parcel GetParcel(int id)
        {
            BO.Parcel p = default;
            try
            {
                var parcel = dal.GetParcel(id);
                if (parcel.DroneId != 0)
                {
                    var drone = DronesList.FirstOrDefault(x => x.Id == parcel.DroneId);
                    p = new BO.Parcel(id, new CustomerInParcel(dal.GetCustomer(parcel.SenderId).Id, dal.GetCustomer(parcel.SenderId).Name), new CustomerInParcel(dal.GetCustomer(parcel.TargetId).Id, dal.GetCustomer(parcel.TargetId).Name), (BO.WeightCategories)parcel.Weight, (BO.Priorities)parcel.priority
                     , new DroneInParcel(drone.Id, drone.Battery, drone.LocationNow), parcel.Requested, parcel.DroneId, parcel.scheduled
                    , parcel.PickedUp, parcel.Delivered);
                }
                else
                {
                    p = new BO.Parcel(id, new CustomerInParcel(dal.GetCustomer(parcel.SenderId).Id, dal.GetCustomer(parcel.SenderId).Name), new CustomerInParcel(dal.GetCustomer(parcel.TargetId).Id, dal.GetCustomer(parcel.TargetId).Name), (BO.WeightCategories)parcel.Weight, (BO.Priorities)parcel.priority
                     , null, parcel.Requested, parcel.DroneId, parcel.scheduled, parcel.PickedUp, parcel.Delivered);
                }
                return p;
            }
            catch (DO.NotExistException ex)
            {
                throw new BO.NotExistException(ex.Message);
            }

        }
        public BO.Station GetStation(int id)
        {
            List<DroneInCharging> d = new();
            try
            {
                var stationDal = dal.GetStation(id);

                foreach (var item in dal.GetListDroneCharges())
                {
                    if (item.Stationld == stationDal.Id)
                    {

                        d.Add(new DroneInCharging(item.Droneld, (DronesList.FirstOrDefault(x => x.Id == item.Droneld)).Battery));
                    }
                }
                return new BO.Station(stationDal.Id, stationDal.Name, new Location(stationDal.Longitude, stationDal.Latitude), stationDal.ChargeSlots, d); ;
            }
            catch (DO.NotExistException ex) { throw new BO.NotExistException(ex.Message); }

        }
    }
}
