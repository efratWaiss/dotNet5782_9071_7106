using System;
using System.Collections.Generic;
using System.Text;

namespace IBL.BO

{
    public class Station
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public int ChargingPositions { get; set; }
        public List<DroneInCharging> DronesInCharging { get; set; }

        public Station(int id, string name, Location location, int chargingPositions, List<DroneInCharging>? dronesInCharging)
        {
            Id = id;
            Name = name;
            Location = location;
            ChargingPositions = chargingPositions;
            DronesInCharging = dronesInCharging;
        }

        public override string ToString()
        {
            return "id:" + Id + " Name:" + Name + " Location:" + Location + " ChargingPositions:" + ChargingPositions + " DronesInCharging:" + string.Join(",", DronesInCharging,"\n");
        }
    }
}
