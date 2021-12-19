using System;
using System.Collections.Generic;
using System.Text;

namespace IBL.BO

{
    public class Drone
    {
        public Drone(int id, string model, WeightCategories maxWeight, DroneStatuses status, double battery, ParcelInTransference parcelInTransference, Location locationNow)
        {
            Id = id;
            Model = model;
            MaxWeight = maxWeight;
            this.status = status;
            this.battery = battery;
            ParcelInTransference = parcelInTransference;
            LocationNow = locationNow;
        }

        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        public DroneStatuses status { get; set; }
        public double battery { get; set; }
        
        public ParcelInTransference ParcelInTransference { get; set; }
        public Location LocationNow { get; set; }
        public override string ToString()
        {
            return "id:" + Id + " Model:" + Model + " MaxWeight:" + MaxWeight;
        }
       
    }
}
