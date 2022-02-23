using System;
using System.Collections.Generic;
using System.Text;


namespace BO

{
    public class Drone
    {
        public Drone(int id, string model, WeightCategories maxWeight, DroneStatuses status, double battery, ParcelInTransference parcelInTransference, Location locationNow)
        {
            Id = id;
            Model = model;
            MaxWeight = maxWeight;
            this.Status = status;
            this.Battery = battery;
            ParcelInTransference = parcelInTransference;
            LocationNow = locationNow;
        }

        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        public DroneStatuses Status { get; set; }
        public double Battery { get; set; }
        public ParcelInTransference ParcelInTransference { get; set; }
        public Location LocationNow { get; set; }
        public override string ToString()
        {
            return "id:" + Id + " Model:" + Model + " MaxWeight:" + MaxWeight + " Status:" + Status + " Battery:" + Battery + " ParcelInTransference:" +
                ParcelInTransference + " LocationNow:" + LocationNow;
        }

    }
}
