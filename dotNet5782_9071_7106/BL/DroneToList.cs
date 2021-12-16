using System;
using System.Collections.Generic;
using System.Text;

namespace IBL.BO
{
    class DroneToList
    {
        public DroneToList(global::System.Int32 id, global::System.String model, WeightCategories maxWeight, DroneStatuses status, Location locationNow, global::System.Int32 parcelDelivered)
        {
            Id = id;
            Model = model;
            MaxWeight = maxWeight;
            this.status = status;
            LocationNow = locationNow;
            ParcelDelivered = parcelDelivered;
        }

        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        //מצב סוללה
        public DroneStatuses status { get; set; }
        public Location LocationNow { get; set; }
        public int ParcelDelivered { get; set; }
        //בנאי וto string
    }
}
