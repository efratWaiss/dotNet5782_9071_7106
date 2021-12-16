using System;
using System.Collections.Generic;
using System.Text;

namespace IBL.BO

{
    public struct Drone
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        //מצב סוללה
        public DroneStatuses status { get; set; }
        public ParcelInTransference ParcelInTransference { get; set; }
        public Location LocationNow { get; set; }
        public override string ToString()
        {
            return "id:" + Id + " Model:" + Model + " MaxWeight:" + MaxWeight;
        }
        public Drone(int id, string Model, WeightCategories MaxWeight, DroneStatuses status)
        {
            this.Id = id;
            this.Model = Model;
            this.MaxWeight = MaxWeight;
            this.status = status;
        }
    }
}
