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
            public int DroneId { get; set; }
            public DateTime scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }
            public static int endParcel;
            public Parcel(int SenderId, int TargetId, WeightCategories Weight, Priorities priority, DateTime Requested,
                DateTime scheduled, DateTime PickedUp, DateTime Delivered, int DroneId=0)
            {
                Id = ++DataSource.Config.parcelId;
                this.SenderId = SenderId;
                this.TargetId = TargetId;
                this.Weight = Weight;
                this.priority = priority;
                this.Requested = Requested;
                this.scheduled = scheduled;
                this.PickedUp = PickedUp;
                this.Delivered = Delivered;
                this.DroneId = DroneId;
            }

            public override string ToString()
            {
                return "id:" + Id + " SenderId:" + SenderId + " TargetId:" + TargetId + " Weight:" + Weight + " priority:" + priority + " Requested:" + Requested + " DroneId:" + DroneId + " scheduled:" + scheduled + " PickedUp:" + PickedUp + " Delivered:" + Delivered;
            }
        }
    }
}
    
