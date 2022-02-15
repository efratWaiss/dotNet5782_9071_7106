using BO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    partial class BL : IBL
    {
        public void SendDroneToStation(int idDrone)
        {
            double min = double.MaxValue;
            double distance = 0;
            var stationHelp = dal.GetListStation().FirstOrDefault(x => x.Id == 1);//אתחול
            var drone = DronesList.FirstOrDefault(x => x.Id == idDrone);
            if (drone != default)
            {
                double wightDrone = droneWeight(drone.Id);
                if (drone.Status == DroneStatuses.Vacant)
                {
                    foreach (var station in dal.GetListStation())
                    {
                        distance = GetDistanceBetweenTwoLocation(new Location(station.Longitude, station.Latitude), drone.LocationNow);
                        if (distance < min && station.ChargeSlots != 0/*לבדוק*/&& drone.Battery > distance * wightDrone * ChargingRate)
                        {
                            stationHelp = station;
                        }
                    }
                    drone.Battery = drone.Battery - (distance * wightDrone * ChargingRate);
                    drone.LocationNow = new Location(stationHelp.Longitude, stationHelp.Latitude);
                    drone.Status = DroneStatuses.Maintenance;
                    stationHelp.ChargeSlots -= 1;//TODO:לבדוק את התחנות הפנויות
                    dal.UpdateStationDetails(stationHelp.Id, stationHelp.Name, stationHelp.ChargeSlots);
                    dal.GetListDroneCharges().ToList().Add(new DO.DroneCharge(drone.Id, stationHelp.Id));
                }
                else
                {
                    throw new NotImplementedException("this drone's status not vacant ");
                }
            }
            else
            {
                throw new NotImplementedException("this Drone's id not exist in the system");
            }
        }
        public void UpdateParcelToDrone(int idDrone)
        {
            try
            {
                double Latitude = 0;
                double Longitude = 0;
                int index = 0;
                int start = 0;
                DO.Parcel temp;
                DO.Parcel parcelChoise = default;
                int end = dal.GetListParcel().Count();
                double minLocation = 99999;
                double tempLocation = 0;
                var p = dal.GetListParcel().ToList();
                List<Parcel> parcels = new List<Parcel>();
                List<Parcel> tempParcels = new List<Parcel>();

                var drone1 = DronesList.FirstOrDefault(x => x.Id == idDrone);
                if (drone1 != default)
                {
                    if (drone1.Status == DroneStatuses.Vacant)//האם הרחפן פנוי 
                    {

                        for (int i = 0; i < dal.GetListParcel().Count(); i++)
                        {//ממיין את הרשימה מעדיפות גבוהה לנמוכה
                            double min = double.MaxValue;
                            foreach (var station in dal.GetListStation())//תחנה קרובה למקבל
                            {
                                if (GetDistanceBetweenTwoLocation(new Location(station.Latitude, station.Longitude), drone1.LocationNow) < min)
                                {
                                    min = GetDistanceBetweenTwoLocation(new Location(station.Latitude, station.Longitude), drone1.LocationNow);
                                    Latitude = station.Latitude;
                                    Longitude = station.Longitude;
                                }
                            }

                            tempLocation =
                                GetDistanceBetweenTwoLocation(new Location(dal.GetCustomer(p[i].SenderId).Longitude, dal.GetCustomer(p[i].SenderId).Latitude), drone1.LocationNow)
                                + GetDistanceBetweenTwoLocation(new Location(dal.GetCustomer(p[i].TargetId).Longitude, dal.GetCustomer(p[i].TargetId).Latitude), drone1.LocationNow)
                              + GetDistanceBetweenTwoLocation(new Location(dal.GetCustomer(p[i].TargetId).Longitude, dal.GetCustomer(p[i].TargetId).Latitude), new Location(Longitude, Latitude));//TODO: check

                            //tempLocation שומר את כל הדרך שעל הרחפן לעבור, כדי להעביר חבילה מלקוח למקבל ולהגיע לתחנה הקרובה לטעינה (כדי לבדוק אם יש לו מספיק בטריה
                            if (tempLocation <= drone1.Battery * ChargingRate * droneWeight(drone1.Id))//בודק האם קיימת מספיק בטריה להמשך הדרך
                            {
                                if ((drone1.MaxWeight == WeightCategories.Intermediate && (WeightCategories)p[i].Weight != WeightCategories.Liver) || (drone1.MaxWeight == WeightCategories.Easy && (WeightCategories)p[i].Weight == WeightCategories.Easy))
                                {
                                    if ((Priorities)p[i].priority == Priorities.Regular)
                                    {
                                        temp = p[end];
                                        p[end] = p[i];
                                        p[i] = temp;
                                        end--;
                                    }
                                    else if ((Priorities)p[i].priority == Priorities.Emergency)
                                    {
                                        temp = p[start];
                                        p[start] = p[i];
                                        p[i] = temp;
                                        start++;
                                    }
                                }
                                else
                                {
                                    p.Remove(p[i]);//מוחק מהרשימה
                                }
                            }
                        }
                        while (p[0].priority == p[index].priority)
                        {
                            index++;
                        }
                        start = 0;
                        end = index;
                        for (int i = 0; i < index; i++)
                        {//ממיין לפי משקל חבילה מרבי
                            if ((WeightCategories)p[i].Weight == WeightCategories.Easy)
                            {
                                temp = p[end];
                                p[end] = p[i];
                                p[i] = temp;
                                end--;
                            }
                            else if ((WeightCategories)p[i].Weight == WeightCategories.Liver)
                            {
                                temp = p[start];
                                p[start] = p[i];
                                p[i] = temp;
                                start++;
                            }
                        }
                        index = 0;
                        while (p[0].Weight == p[index].Weight)
                        {
                            index++;
                        }
                        for (int i = 0; i < index; i++)
                        {
                            tempLocation = GetDistanceBetweenTwoLocation(
                                new Location(dal.GetCustomer(p[i].SenderId).Longitude, dal.GetCustomer(p[i].SenderId).Latitude)
                                , drone1.LocationNow);
                            if (minLocation > tempLocation)
                            {
                                minLocation = tempLocation;
                                parcelChoise = p[i];
                            }
                        }
                        if (parcelChoise.Equals(default))
                        {
                            drone1.Status = DroneStatuses.Shipping;
                            parcelChoise.DroneId = drone1.Id;
                            parcelChoise.Delivered = DateTime.Now;
                        }
                    }
                }

                else
                {
                    throw new NotImplementedException("this Drone's id not exist in the system");//TODO:exception
                }

            }
            catch (DO.IdException ex) { throw new IdException(ex.Message); }
        }

        public void PackageCollectionByDrone(int idDrone)
        {
            try
            {
                bool flag = false;
                var pacelsL = dal.GetListParcel().ToList();
                var drone = dal.GetDrone(idDrone);
                DroneToList GetListDrone = DronesList.FirstOrDefault(x => x.Id == idDrone);
                var itemParcel = dal.GetParcel(GetListDrone.ParcelDelivered);
                for (int i = 0; i < dal.GetListParcel().Count() && flag == false; i++)
                {

                    if (pacelsL[i].DroneId == idDrone && pacelsL[i].PickedUp.Equals(default))
                    {
                        flag = true;
                        GetListDrone.Battery = GetListDrone.Battery - (GetDistanceBetweenTwoLocation(GetListDrone.LocationNow,
                             new Location(dal.GetCustomer(pacelsL[i].SenderId).Longitude, dal.GetCustomer(pacelsL[i].TargetId).Latitude))) * ChargingRate * droneWeight(pacelsL[i].DroneId);
                        GetListDrone.LocationNow = new Location(dal.GetCustomer(pacelsL[i].SenderId).Longitude, dal.GetCustomer(pacelsL[i].TargetId).Latitude);
                        itemParcel.PickedUp = DateTime.Now;
                        dal.UpdateParcel(itemParcel);
                    }

                }
                if (flag == false)
                {

                    throw new NotImplementedException("The package was not associated with this skimmer or the package has already been collected");
                }
            }
            catch (DO.IdException ex) { throw new IdException(ex.Message); }
        }
        public void DeliveryOfAParcelByDrone(int idDrone)
        {
            try
            {
                bool flag = false;

                var parcels = dal.GetListParcel().ToList();
                var drone = dal.GetDrone(idDrone);
                DroneToList GetListDrone = DronesList.FirstOrDefault(x => x.Id == idDrone);
                var itemParcel = dal.GetParcel(GetListDrone.ParcelDelivered);
                for (int i = 0; i < dal.GetListParcel().Count() && flag == false; i++)
                {
                    if (parcels[i].DroneId == idDrone)
                    {

                        if (!parcels[i].PickedUp.Equals(default) && parcels[i].Delivered.Equals(default))
                        {
                            flag = true;
                            double v = dal.GetCustomer(parcels[i].TargetId).Longitude;
                            double v1 = dal.GetCustomer(parcels[i].TargetId).Latitude;
                            double b = GetDistanceBetweenTwoLocation(GetListDrone.LocationNow,
                            new Location(dal.GetCustomer(parcels[i].TargetId).Longitude, dal.GetCustomer(parcels[i].TargetId).Latitude));
                            GetListDrone.Battery = GetListDrone.Battery - ((GetDistanceBetweenTwoLocation(GetListDrone.LocationNow,
                            new Location(dal.GetCustomer(parcels[i].TargetId).Longitude, dal.GetCustomer(parcels[i].TargetId).Latitude))) * ChargingRate * droneWeight(idDrone));
                            GetListDrone.LocationNow = new Location(dal.GetCustomer(parcels[i].SenderId).Longitude, dal.GetCustomer(parcels[i].SenderId).Latitude);
                            GetListDrone.Status = DroneStatuses.Vacant;
                            itemParcel.Delivered = DateTime.Now;
                            dal.UpdateParcel(itemParcel);
                        }

                    }
                    else
                    {
                        flag = false;
                    }

                }
                if (flag == false)
                {
                    throw new NotImplementedException("The package has not been collected or the package has already been delivered or the skimmer is not associated with any package");
                }
            }
            catch (DO.IdException ex) { throw new IdException(ex.Message); }
        }

        public void DeleateParcel(int Id)
        {
            ParcelToList p;
            
        }
        public void UpdateNameDrone(int Id, String Model)
        {
            try
            {
                var Drone = dal.GetDrone(Id);
                Drone.Model = Model;
                var Drone1 = DronesList.FirstOrDefault(x => x.Id == Id);
                Drone1.Model = Model;
            }
            catch (DO.IdException ex) { throw new IdException(ex.Message); }

        }
        public void UpdateStationDetails(int IdStation, String? NameStation, int ChargeSlots)
        {
            try
            {
                var station = GetListStation().FirstOrDefault(x => x.Id == IdStation);


                if (!NameStation.Equals(default) && !ChargeSlots.Equals(default))
                {

                    station.Name = NameStation;
                    station.OccupanciesChargingPositions = ChargeSlots;
                }
                else
                {
                    throw new Exception("you didnt put good values");
                }


            }
            catch (DO.IdException ex) { throw new IdException(ex.Message); }

        }
        public void UpdateCustomerDetails(int IdCustomer, String? Name, String? Phone)
        {
            try
            {

                var customer1 = GetListCustomer().FirstOrDefault(x => x.Identity == IdCustomer);
                if (!Name.Equals(default) && !Phone.Equals(default))
                {
                    customer1.Name = Name;
                    customer1.Phone = Phone;

                }
                else { throw new Exception("you didnt put good values"); }

            }
            catch (DO.IdException ex)
            {
                throw new IdException(ex.Message);
            }
        }
        public void FreeDrone(int idDrone, double timeInCharging)
        {
            DO.Station station;
            var drone = DronesList.FirstOrDefault(x => x.Id == idDrone);
            if (drone != default)
            {
                if (drone.Status == DroneStatuses.Maintenance)
                {
                    drone.Battery += timeInCharging * ChargingRate * droneWeight(drone.Id);
                    drone.Status = DroneStatuses.Vacant;
                    try
                    {
                        station = dal.GetStation(drone.ParcelDelivered);
                        station.ChargeSlots += 1;
                    }
                    catch
                    {
                        throw new NotImplementedException("the drone's parcel did not Delivered");
                    }
                    try
                    {
                        dal.removeFromDroneCharges(idDrone, station.Id);
                    }
                    catch (DO.IdException ex)
                    {
                        throw new IdException(ex.Message);
                    }

                }
                else
                {
                    throw new NotImplementedException("the drone's status is not Maintenance");
                }
            }
            else
            {
                throw new NotImplementedException("this id drone not exist in the system");
            }

        }
    }
}
    

