using System;
using System.Collections.Generic;
using System.Text;

namespace IBL.BO

{
    class Station
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public int ChargingPositions { get; set; }
        public List<DroneInCharging> DronesInCharging { get; set; }
        public Station(int Id, string Name, Location Location, int ChargingPositions, List<DroneInCharging> DronesInCharging)
        {
            this.Id = Id;
            this.Name = Name;
            this.Location = Location;
            this.ChargingPositions = ChargingPositions;
            this.DronesInCharging = DroneInCharging;
        }

        public override string ToString()
        {
            return "id:" + Id + " Name:" + Name + " Location:" + Location + " ChargingPositions:" + ChargingPositions + " DronesInCharging:" + DronesInCharging;
        }
    }
}
