using System;
using System.Collections.Generic;
using System.Text;

namespace IBL.BO

{
   public class Parcel
    {
        public static int Id { get; set; }
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
            DateTime Affiliation, DateTime PickedUp, DateTime Supply,DroneInParcel DroneInParcel=0)
        {
            Id = ++Id; 
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
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
