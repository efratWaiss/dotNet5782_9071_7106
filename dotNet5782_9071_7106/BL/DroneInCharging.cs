using System;
using System.Collections.Generic;
using System.Text;

namespace IBL.BO
{
   public class DroneInCharging
    {
        public DroneInCharging(int id, double battery)
        {
            Id = id;
            this.battery = battery;
        }

        public int Id { get; set; }

        public double battery { get; set; }
        public override string ToString()
        {
            return base.ToString();
        }

    }
}
