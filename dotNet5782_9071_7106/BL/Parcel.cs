using System;
using System.Collections.Generic;
using System.Text;

namespace IBL.BO

{
    class Parcel
    {
        public int Id { get; set; }
        public CustomerInParcel Sender { get; set; }
        public CustomerInParcel Target { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities priority { get; set; }
        public DateTime ParcelCreationTime { get; set; }
        public DroneInParcel DroneInParcel { get; set; }
        public DateTime Affiliation { get; set; }
        public DateTime PickedUp { get; set; }
        public DateTime Supply
        {

            get {return DateTime.Now;} 
            set { Supply = value; }
            
            }

        //public static int endParcel;
        public Parcel(CustomerInParcel Sender, CustomerInParcel Target, WeightCategories Weight, Priorities priority, DateTime ParcelCreationTime,
           DroneInParcel DroneInParcel = 0, DateTime Affiliation, DateTime PickedUp, DateTime Supply)
        {
            Id = ++DataSource.Config.parcelId;//איך לשמור את ה id
            this.Sender = Sender;
            this.Target = Target;
            this.Weight = Weight;
            this.priority = priority;
            this.ParcelCreationTime = ParcelCreationTime;
            this.DroneInParcel = DroneInParcel;
            this.Affiliation = Affiliation;
            this.PickedUp = PickedUp;
            this.Supply = Supply;
        }
        // לשנות את הto string
        public override string ToString()
        {
            return "id:" + Id + " SenderId:" + SenderId + " TargetId:" + TargetId + " Weight:" + Weight + " priority:" + priority + " Requested:" + Requested + " DroneId:" + DroneId + " scheduled:" + scheduled + " PickedUp:" + PickedUp + " Delivered:" + Delivered;
        }
    }
}
