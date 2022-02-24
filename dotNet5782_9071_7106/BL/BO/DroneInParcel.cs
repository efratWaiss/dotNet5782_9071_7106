using System;
using System.Collections.Generic;
using System.Text;


namespace BO
{
    public class DroneInParcel
    {
        public DroneInParcel(int id, double battery, Location locationNow)
        {
            Id = id;
            this.Battery = battery;
            LocationNow = locationNow;
        }

        public int Id { get; set; }

        public double Battery { get; set; }
        public Location LocationNow { get; set; }

        public override string ToString()
        {
            return "Id:" + Id + " Battery:" + Battery + " LocationNow:" + LocationNow;
        }
    }
}
