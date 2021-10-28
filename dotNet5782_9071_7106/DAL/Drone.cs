using System;
namespace IDAL
{
    namespace DO
    {
        public struct Drone
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories MaxWeight { get; set; }
            public DroneStatuses Status { get; set; }
            public double Battery { get; set; }
            public override string ToString()
            {
                return "id:" + Id + " Model:" + Model + " MaxWeight:" + MaxWeight + " Battery:" + Battery;
            }
            public Drone(int id, string Model, WeightCategories MaxWeight, DroneStatuses Status, double Battery)
            {
                this.Id = id;
                this.Model = Model;
                this.MaxWeight = MaxWeight;
                this.Status = Status;
                this.Battery = Battery;

            }
        }
    }
}