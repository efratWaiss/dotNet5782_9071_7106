using System;
using System.Collections.Generic;
using System.Text;

namespace IBL.BO
{
    class ParcelInTransference
    {
        public int Id { get; set; }
        public CustomerInParcel Sender { get; set; }
        public CustomerInParcel Target { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities priority { get; set; }
        public bool CollectionOrTarget { get; set; }
        public Location LocationCollection { get; set; }
        public Location LocationTarget { get; set; }
        public double TransportDistance { get; set; }
        //בנאי והדפסה
    }
}
