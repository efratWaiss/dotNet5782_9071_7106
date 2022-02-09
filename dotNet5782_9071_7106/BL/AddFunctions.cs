﻿using BO;
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
        public void AddCustomer(int Id, String Name, String Phone, double Longitude, double Latitude)
        {
            try
            {
                dal.AddCustomer(new DO.Customer(Id, Name, Phone, Longitude, Latitude));
            }
            catch (DO.IdException ex)
            {
               throw new IdException(ex.Message);
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
                    dal.AddDrone(new DO.Drone(Id, Model, (DO.WeightCategories)(weight)));
                    DronesList.Add(new DroneToList(Id, Model, weight, battery, DroneStatuses.Maintenance, new Location(Station1.Latitude, Station1.Longitude), -1));
                }
                catch (DO.IdException ex) { throw new IdException(ex.Message);}
            }
            catch (DO.IdException ex) { throw new IdException(ex.Message); }

        }
        public int AddParcel(int SenderId, int TargetId, WeightCategories Weight, Priorities Priorities)
        {
            try
            {
                var customer1 = dal.GetCustomer(SenderId);
                var customer2 = dal.GetCustomer(TargetId);
                return dal.AddParcel(new DO.Parcel(SenderId, TargetId, (DO.WeightCategories)Weight, (DO.Priorities)Priorities, DateTime.Now, 0, null, null, null));
            }
            catch (DO.IdException ex) { throw new IdException(ex.Message);}


        }
        public void AddStation(int Id, String Name, Location location, int AvailableChargeSlots)
        {
            try
            {

                dal.AddStation(new DO.Station(Id, Name, location.Latitude, location.Longitude, AvailableChargeSlots));
            }

            catch (DO.IdException ex)
            { // :אם מצאת כבר רחפן כזה, זרוק 
                throw new IdException(ex.Message);
            }

        }

    }
}
