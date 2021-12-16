using System;
using System.Collections.Generic;
using System.Text;

namespace IBL.BO
{
    class DroneInParcel
    {
        public DroneInParcel(global::System.Int32 id, Location locationNow)
        {
            Id = id;
            LocationNow = locationNow;
        }

        public int Id { get; set; }
       
        //מצב סוללה
        public Location LocationNow { get; set; }

        //להוסיף בנאי ו toString 
    }
}
