using System;
using System.Collections.Generic;
using System.Text;

namespace IBL.BO
{
    public class DroneToList
    {
        public DroneToList(int id, string model, WeightCategories maxWeight, double battery, DroneStatuses status, Location locationNow, int parcelDelivered)
        {
            Id = id;
            Model = model;
            MaxWeight = maxWeight;
            this.battery = battery;
            this.status = status;
            LocationNow = locationNow;
            ParcelDelivered = parcelDelivered;
        }

        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        public double battery { get; set; }
        public DroneStatuses status { get; set; }
        public Location LocationNow { get; set; }
        public int ParcelDelivered { get; set; }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
