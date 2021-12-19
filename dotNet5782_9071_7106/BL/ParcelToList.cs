using System;
using System.Collections.Generic;
using System.Text;

namespace IBL.BO
{
   public class ParcelToList
    {
        public ParcelToList(int id, CustomerInParcel sender, CustomerInParcel target, WeightCategories weight, Priorities priority, ParcelStatsus parcelStatsus)
        {
            Id = id;
            Sender = sender;
            Target = target;
            Weight = weight;
            this.priority = priority;
            ParcelStatsus = parcelStatsus;
        }

        public int Id { get; set; }
        public CustomerInParcel Sender { get; set; }
        public CustomerInParcel Target { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities priority { get; set; }
        public ParcelStatsus ParcelStatsus { get; set; }
        public override string ToString()
        {
            return base.ToString();
        }

    }
}
