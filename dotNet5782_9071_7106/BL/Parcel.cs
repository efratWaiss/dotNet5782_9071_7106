using System;
using System.Collections.Generic;
using System.Text;

namespace IBL.BO

{
    public class Parcel
    {
        //public static int Id { get; set; }
        public int ParcelId { get; set; }
        public CustomerInParcel Sender { get; set; }
        public CustomerInParcel Target { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public DroneInParcel DroneInParcel { get; set; }
        public DateTime? Requested { get; set; }
        public int DroneId { get; set; }
        public DateTime? scheduled { get; set; }
        public DateTime? PickedUp { get; set; }
        public DateTime? Delivered { get; set; }//TODO:
        //{
        //    get { return DateTime.Now; }
        //    set { Delivered = value; }
        //}

        public Parcel(int ID,CustomerInParcel Sender, CustomerInParcel Target, WeightCategories Weight, Priorities Priority, DroneInParcel DroneInParcel, DateTime? Requested, int DroneId, DateTime? scheduled,  DateTime? PickedUp, DateTime? Delivered)
        {
            this.ParcelId = ID;
            this.Sender = Sender;
            this.Target = Target;
            this.Weight = Weight;
            this.Priority = Priority;
            this.DroneInParcel = DroneInParcel;
            this.Requested = Requested;
            this.DroneId = DroneId;
            this.scheduled = scheduled;
            this.PickedUp = PickedUp;
            this.Delivered = Delivered;
        }


        public override string ToString()
        {
            return "ParcelId:" + ParcelId + " Sender:" + Sender + " Target:" + Target + " Weight:" + Weight + " Priority:" + Priority + " DroneInParcel:"
                + DroneInParcel + " Requested:" + Requested + " DroneId:" + DroneId + " scheduled:" + scheduled + " PickedUp:" + PickedUp+ " Delivered:"+ Delivered;
        }
    }
}
