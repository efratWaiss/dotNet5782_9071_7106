using System;
using System.Collections.Generic;
using System.Text;

namespace IBL.BO

{
    class Station
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int ChargeSlots { get; set; }
        public Station(int Id, string Name, double Longitude, double Latitude, int ChargeSlots = 0)
        {
            this.Id = Id;
            this.Name = Name;
            this.Longitude = Longitude;
            this.Latitude = Latitude;
            this.ChargeSlots = ChargeSlots;
        }

        public override string ToString()
        {
            return "id:" + Id + " Name:" + Name + " Longitude:" + Longitude + " Latitude:" + Latitude + " ChargeSlots:" + ChargeSlots;
        }
    }
}
