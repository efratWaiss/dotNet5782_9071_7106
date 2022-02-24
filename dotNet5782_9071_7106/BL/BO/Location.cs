﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class Location
    {
        public Location(double longitude, double latitude)
        {
            this.Longitude = longitude;
            this.Latitude = latitude;
        }

        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public override string ToString()
        {
            return "Longitude:" + Longitude + " Latitude:" + Latitude;
        }
    }
}
