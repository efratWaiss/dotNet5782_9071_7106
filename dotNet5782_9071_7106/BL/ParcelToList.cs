﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IBL.BO
{
    class ParcelToList
    {
        public int Id { get; set; }
        public CustomerInParcel Sender { get; set; }
        public CustomerInParcel Target { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities priority { get; set; }
        public ParcelStatsus ParcelStatsus { get; set; }
        public ParcelToList(int Id, CustomerInParcel Sender, CustomerInParcel Target, WeightCategories Weight, Priorities priority, ParcelStatsus ParcelStatsus)
        {
            this.Id = Id;
            this.Weight = Weight;
            this.priority = priority;
            this.Sender = Sender;
            this.Target = Target;
            this.ParcelStatsus = ParcelStatsus;
        }
        override T
    }// לא ךשכוח לעשות tostring
}
