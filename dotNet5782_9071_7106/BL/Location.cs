using System;
using System.Collections.Generic;
using System.Text;

namespace IBL.BO
{
    class Location
    {
        public Location(global::System.Double longitude, global::System.Double latitude)
        {
            this.longitude = longitude;
            this.latitude = latitude;
        }

        double longitude { get; set; }
        double latitude { get; set; }
    }
}
