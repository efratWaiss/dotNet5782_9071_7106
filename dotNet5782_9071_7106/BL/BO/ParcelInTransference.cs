using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class ParcelInTransference
    {
        public ParcelInTransference(int id, CustomerInParcel sender, CustomerInParcel target, WeightCategories weight, Priorities priority, bool collectionOrTarget, Location locationCollection, Location locationTarget, double transportDistance)
        {
            Id = id;
            Sender = sender;
            Target = target;
            Weight = weight;
            this.Priority = priority;
            CollectionOrTarget = collectionOrTarget;
            LocationCollection = locationCollection;
            LocationTarget = locationTarget;
            TransportDistance = transportDistance;
        }

        public int Id { get; set; }
        public CustomerInParcel Sender { get; set; }
        public CustomerInParcel Target { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public bool CollectionOrTarget { get; set; }
        public Location LocationCollection { get; set; }
        public Location LocationTarget { get; set; }
        public double TransportDistance { get; set; }
        public override string ToString()
        {
            return "Id:" + Id + " Sender:" + Sender + " Target:" + Target + " Weight:" + Weight + " Priority:" + Priority + " CollectionOrTarget:" + CollectionOrTarget
                + " LocationCollection:" + LocationCollection + " LocationTarget:" + LocationTarget + " TransportDistance:" + TransportDistance;
        }
    }
}
