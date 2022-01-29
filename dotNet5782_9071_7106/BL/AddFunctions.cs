using DalApi;
using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public partial class BL : IBL
    {
        public void AddCustomer(int Id, String Name, String Phone, double Longitude, double Latitude)
        {
            try
            {
                dal.AddCustomer(new IDAL.DO.Customer(Id, Name, Phone, Longitude, Latitude));
            }
            catch (IdException ex)
            {
                throw (ex);
            }

        }
        public void AddDrone(int Id, String Model, WeightCategories weight, int IdStation)
        {
            try
            {

                Random rn = new Random();
                double battery = rn.Next(20, 41);
                try
                {
                    var Station1 = dal.GetStation(IdStation);
                    dal.AddDrone(new IDAL.DO.Drone(Id, Model, (IDAL.DO.WeightCategories)(weight)));
                    DronesList.Add(new DroneToList(Id, Model, weight, battery, DroneStatuses.Maintenance, new Location(Station1.Latitude, Station1.Longitude), -1));
                }
                catch (IdException e)
                {
                    throw e;
                }
            }
            catch (IdException e)
            {
                throw (e);
            }

        }
        public int AddParcel(int SenderId, int TargetId, WeightCategories Weight, Priorities Priorities)
        {
            try
            {
                var customer1 = dal.GetCustomer(SenderId);
                var customer2 = dal.GetCustomer(TargetId);
                return dal.AddParcel(new IDAL.DO.Parcel(SenderId, TargetId, (IDAL.DO.WeightCategories)Weight, (IDAL.DO.Priorities)Priorities, DateTime.Now, 0, null, null, null));
            }
            catch (IdException e)
            {
                throw (e);
            }


        }
        public void AddStation(int Id, String Name, Location location, int AvailableChargeSlots)
        {
            try
            {

                dal.AddStation(new IDAL.DO.Station(Id, Name, location.Latitude, location.Longitude, AvailableChargeSlots));
            }

            catch (IdException e)
            { // :אם מצאת כבר רחפן כזה, זרוק 
                throw (e);
            }

        }

    }
}
