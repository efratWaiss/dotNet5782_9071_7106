﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IBL.BO
{
    class ParcelInCustomer
    {
        public ParcelInCustomer(global::System.Int32 id, WeightCategories weight, Priorities priority, ParcelStatsus parcelStatsus, CustomerInParcel sender, CustomerInParcel target)
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
    }
    public ParcelInCustomer(int Id, CustomerInParcel Sender, CustomerInParcel Target, WeightCategories Weight, Priorities priority, ParcelStatsus ParcelStatsus)
    {
        this.Id = Id;
        this.Weight = Weight;
        this.priority = priority;
        this.Sender = Sender;
        this.Target = Target;
        this.ParcelStatsus = ParcelStatsus;
    }
    // לא ךשכוח לעשות tostring
}
