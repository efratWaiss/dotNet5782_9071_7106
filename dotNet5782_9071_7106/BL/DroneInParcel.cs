using System;
using System.Collections.Generic;
using System.Text;

namespace IBL.BO
{
    public class DroneInParcel
    {
        public DroneInParcel(int id, double battery, Location locationNow)
        {
            Id = id;
            this.battery = battery;
            LocationNow = locationNow;
        }

        public int Id { get; set; }

        public double battery { get; set; }
        public Location LocationNow { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
