using System;
namespace IDAL
{
    namespace DO
    {
        public struct Parcel
        {
            public int Id { get; set; }
            public int SenderId { get; set; } 
            public int TargetId { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities priority { get; set; }
            public DateTime Requested { get; set; }
            public int DroneId { get { return DroneId; } set { DroneId = 0; } }
            public DateTime scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }
            public override string ToString()
            {
                return "id:" + Id + " SenderId:" + SenderId + " TargetId:" + TargetId + " Weight:" + Weight+ " priority:"+ priority+ " Requested:"+ Requested+ " DroneId:"+ DroneId+ " scheduled:"+ scheduled+ " PickedUp:"+ PickedUp+ " Delivered:" + Delivered;
            }
        }
    }
}