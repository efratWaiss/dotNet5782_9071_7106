using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class ParcelToList
    {
        public ParcelToList(int id, CustomerInParcel sender, CustomerInParcel target, WeightCategories weight, Priorities priority, ParcelStatsus parcelStatsus)
        {
            Id = id;
            Sender = sender;
            Target = target;
            Weight = weight;
            this.Priority = priority;
            ParcelStatsus = parcelStatsus;
        }

        public int Id { get; set; }
        public CustomerInParcel Sender { get; set; }
        public CustomerInParcel Target { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public ParcelStatsus ParcelStatsus { get; set; }
        public override string ToString()
        {
            return "Id:" + Id + " Sender:" + Sender + " Target:" + Target + " Weight:" + Weight + " Priority:" + Priority + " ParcelStatsus:" + ParcelStatsus;
        }

    }
}
