using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class DroneInCharging
    {
        public DroneInCharging(int id, double battery)
        {
            Id = id;
            this.Battery = battery;
        }
        public int Id { get; set; }

        public double Battery { get; set; }
        public override string ToString()
        {
            return "Id:" + Id + " Battery:" + Battery;
        }

    }
}
