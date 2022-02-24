using System;

    namespace DO
    {
        public struct DroneCharge
        {
            public int Droneld { get; set; }
            public int Stationld { get; set; }
            public override string ToString()
            {
                return "Droneld:"+ Droneld + " Stationld:" + Stationld;
            }
            public DroneCharge(int Droneld, int Stationld)
            {
                this.Droneld = Droneld;
                this.Stationld = Stationld;
            }
        }
    }
