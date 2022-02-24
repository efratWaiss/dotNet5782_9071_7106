using System;
using System.Collections.Generic;
using System.Text;


namespace BO
{
    public class DroneToList
    {
        public DroneToList(int id, string model, WeightCategories maxWeight, double battery, DroneStatuses status, Location locationNow, int parcelDelivered)
        {
            Id = id;
            Model = model;
            MaxWeight = maxWeight;
            this.Battery = battery;
            this.Status = status;
            LocationNow = locationNow;
            ParcelDelivered = parcelDelivered;
        }

        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        public double Battery { get; set; }
        public DroneStatuses Status { get; set; }
        public Location LocationNow { get; set; }
        public int ParcelDelivered { get; set; }
        public override string ToString()
        {
            return "Id:" + Id + " Model:" + Model + " MaxWeight:" + MaxWeight + " Battery:" + Battery + " Status:" + Status + " LocationNow:" +
                LocationNow + " ParcelDelivered:" + ParcelDelivered;
        }
    }
}
