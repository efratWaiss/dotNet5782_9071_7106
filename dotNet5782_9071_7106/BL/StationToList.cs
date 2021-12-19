using System;
using System.Collections.Generic;
using System.Text;

namespace IBL.BO
{
   public class StationToList
    {
       

        public int Id { get; set; }
        public string Name { get; set; }
        public int AvailableChargingPositions { get; set; }
        public int OccupanciesChargingPositions { get; set; }
       
         public StationToList(int id, string name, int availableChargingPositions, int occupanciesChargingPositions)
        {
            Id = id;
            Name = name;
            AvailableChargingPositions = availableChargingPositions;
            OccupanciesChargingPositions = occupanciesChargingPositions;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
