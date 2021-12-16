using System;
using System.Collections.Generic;
using System.Text;

namespace namespace IBL.BO
{
    class DroneToList
    {
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
