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
            this.priority = priority;
            ParcelStatsus = parcelStatsus;
            Sender = sender;
            Target = target;
        }

        public int Id { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities priority { get; set; }
        public ParcelStatsus ParcelStatsus { get; set; }
        public CustomerInParcel Sender { get; set; }
        public CustomerInParcel Target { get; set; }
        public override string ToString()
        {
            return base.ToString();
        }
    }

    
}
