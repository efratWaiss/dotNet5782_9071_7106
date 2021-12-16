using System;
using System.Collections.Generic;
using System.Text;

namespace IBL.BO
{
    class StationToList
    {
        public StationToList(global::System.Int32 id, global::System.String name, global::System.Int32 availableChargingPositions, global::System.Int32 occupanciesChargingPositions)
        {
            Id = id;
            Name = name;
            AvailableChargingPositions = availableChargingPositions;
            OccupanciesChargingPositions = occupanciesChargingPositions;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int AvailableChargingPositions { get; set; }
        public int OccupanciesChargingPositions { get; set; }
       
        
        }
}
