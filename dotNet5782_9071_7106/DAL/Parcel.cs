using System;
namespace IDAL
{
    namespace DO
    {
        public struct DroneCharge
        {
            public int Id { get; set; }
            public int SenderId { get; set; 
            public int TargetId { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities priority { get; set; }
            public DateTime Requested { get; set; }
            public int Droneld { get; set; }
            public DateTime scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }
        }
    }
}