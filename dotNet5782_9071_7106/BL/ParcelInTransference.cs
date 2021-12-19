using System;
using System.Collections.Generic;
using System.Text;

namespace IBL.BO
{
   public class ParcelInTransference
    {
        public ParcelInTransference(int id, CustomerInParcel sender, CustomerInParcel target, WeightCategories weight, Priorities priority, bool collectionOrTarget, Location locationCollection, Location locationTarget, double transportDistance)
        {
            Id = id;
            Sender = sender;
            Target = target;
            Weight = weight;
            this.priority = priority;
            CollectionOrTarget = collectionOrTarget;
            LocationCollection = locationCollection;
            LocationTarget = locationTarget;
            TransportDistance = transportDistance;
        }

        public int Id { get; set; }
        public CustomerInParcel Sender { get; set; }
        public CustomerInParcel Target { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities priority { get; set; }
        public bool CollectionOrTarget { get; set; }
        public Location LocationCollection { get; set; }
        public Location LocationTarget { get; set; }
        public double TransportDistance { get; set; }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
