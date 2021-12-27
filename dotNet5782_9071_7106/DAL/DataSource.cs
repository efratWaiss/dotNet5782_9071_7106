using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
namespace IDAL
{
    namespace DO
    {
        internal class DataSource
        {
            internal static List<Drone> drones = new List<Drone>();
            internal static List<Station> stations = new List<Station>();
            internal static List<Customer> customers = new List<Customer>();
            internal static List<Parcel> parcels = new List<Parcel>();
            internal static List<DroneCharge> DroneCharges = new List<DroneCharge>();


            internal class Config
            {
                internal static int parcelId =0;
                public static double available;
                public static double lightWeight;
                public static double MediumWeight;
                public static double heavyWeight;
                public static int ChargingRate;

            }
            internal static void Initialize()
            {
                Drone d1=new Drone(1,"xyz122",WeightCategories.Easy);
                Drone d2=new Drone(2,"xyz122",WeightCategories.Intermediate);
                Drone d3=new Drone(3,"xyz122",WeightCategories.Intermediate);
                Drone d4=new Drone(4,"xyz122",WeightCategories.Intermediate);
                Drone d5=new Drone(5,"xyz122",WeightCategories.Intermediate);
                drones.Add(d1); 
                drones.Add(d2); 
                drones.Add(d3); 
                drones.Add(d4); 
                drones.Add(d5);
                Station s1 = new Station(54, "sfsf", 12.5, 14.6, 7);
                Station s2 = new Station(54, "sfsf", 12.5, 14.6, 7);
                stations.Add(s1);
                stations.Add(s2);
                Customer c1 = new Customer(325036551, "gfdx", "4545454", 12.3, 145.5);
                Customer c2 = new Customer(325036552, "gfdx", "4545454", 12.3, 145.5);
                Customer c3 = new Customer(325036553, "gfdx", "4545454", 12.3, 145.5);
                Customer c4 = new Customer(325036554, "gfdx", "4545454", 12.3, 145.5);
                Customer c5 = new Customer(325036555, "gfdx", "4545454", 12.3, 145.5);
                Customer c6 = new Customer(325036556, "gfdx", "4545454", 12.3, 145.5);
                Customer c7 = new Customer(325036557, "gfdx", "4545454", 12.3, 145.5);
                Customer c8 = new Customer(325036558, "gfdx", "4545454", 12.3, 145.5);
                Customer c9 = new Customer(325036559, "gfdx", "4545454", 12.3, 145.5);
                Customer c10 = new Customer(325036550, "gfdx", "4545454", 12.3, 145.5);
                customers.Add(c1);
                customers.Add(c2);
                customers.Add(c3);
                customers.Add(c4);
                customers.Add(c5);
                customers.Add(c6);
                customers.Add(c7);
                customers.Add(c8);
                customers.Add(c9);
                customers.Add(c10);
                DateTime t = DateTime.Now;
                Parcel p1 = new Parcel( 12, 5, WeightCategories.Liver, Priorities.Regular,DateTime.Now,0 );
                Parcel p2 = new Parcel( 12, 5, WeightCategories.Liver, Priorities.Regular,DateTime.Now,0 );
                Parcel p3 = new Parcel( 12, 5, WeightCategories.Liver, Priorities.Regular,DateTime.Now,0 );
                Parcel p4 = new Parcel( 12, 5, WeightCategories.Liver, Priorities.Regular,DateTime.Now,0 );
                Parcel p5 = new Parcel( 12, 5, WeightCategories.Liver, Priorities.Regular,DateTime.Now,0 );
                Parcel p6 = new Parcel( 12, 5, WeightCategories.Liver, Priorities.Regular,DateTime.Now,0 );
                Parcel p7 = new Parcel( 12, 5, WeightCategories.Liver, Priorities.Regular,DateTime.Now,0 );
                Parcel p8 = new Parcel( 12, 5, WeightCategories.Liver, Priorities.Regular,DateTime.Now,0 );
                Parcel p9 = new Parcel( 12, 5, WeightCategories.Liver, Priorities.Regular,DateTime.Now,0 );
                Parcel p10 = new Parcel( 12, 5, WeightCategories.Liver, Priorities.Regular,DateTime.Now,0 );
                Parcel p11 = new Parcel( 12, 5, WeightCategories.Liver, Priorities.Regular,DateTime.Now,0 );
                Parcel p12 = new Parcel( 12, 5, WeightCategories.Liver, Priorities.Regular,DateTime.Now,0 );
                
               









            }
        }

    }
}
