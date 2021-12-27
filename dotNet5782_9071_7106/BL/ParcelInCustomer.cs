using System;
using System.Collections.Generic;
using System.Text;

namespace IBL.BO
{
    public class ParcelInCustomer
    {
        public ParcelInCustomer(int id, WeightCategories weight, Priorities priority, ParcelStatsus parcelStatsus, CustomerInParcel sender, CustomerInParcel target)
        {
            Id = id;
            Weight = weight;
            this.Priority = priority;
            ParcelStatsus = parcelStatsus;
            Sender = sender;
            Target = target;
        }

        public int Id { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public ParcelStatsus ParcelStatsus { get; set; }
        public CustomerInParcel Sender { get; set; }
        public CustomerInParcel Target { get; set; }
        public override string ToString()
        {
            return "Id:" + Id + " Weight:" + Weight + " Priority:" + Priority + " ParcelStatsus:" + ParcelStatsus + " Sender:" + Sender + " Target:" + Target;
        }
    }


}
