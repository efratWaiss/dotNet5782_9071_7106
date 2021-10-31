using System;
namespace IDAL
{
    namespace DO
    {
        internal class DataSource
        {
            public Drone[] drones = new Drone[10];
            public Station[] stations = new Station[5];
            public Customer[] customers = new Customer[100];
            public Parcel[] parcels = new Parcel[1000];
            internal class Config
            {
                internal public static int drone_index = 0;
                internal public static int station_index = 0;
                internal public static int customer_index = 0;
                internal public static int parcel_index = 0;
            }
        }
    }
}