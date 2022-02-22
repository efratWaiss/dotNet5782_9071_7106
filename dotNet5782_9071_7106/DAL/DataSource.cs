using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

    namespace DO
    {
        public class DataSource
        {
            internal static List<Drone> drones = new List<Drone>();
            internal static List<Station> stations = new List<Station>();
            internal static List<Customer> customers = new List<Customer>();
            internal static List<Parcel> parcels = new List<Parcel>();
            internal static List<DroneCharge> DroneCharges = new List<DroneCharge>();


            public class Config
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
                Drone d2=new Drone(2,"xcz122",WeightCategories.Intermediate);
                Drone d3=new Drone(3,"xyz122",WeightCategories.Liver);
                Drone d4=new Drone(4,"xyz122",WeightCategories.Intermediate);
                Drone d5=new Drone(5,"xyz122",WeightCategories.Liver);
                drones.Add(d1); 
                drones.Add(d2); 
                drones.Add(d3); 
                drones.Add(d4); 
                drones.Add(d5);
                Station s1 = new Station(52, "sfsf", 12.5, 14.6,0);
                Station s2 = new Station(54, "sfsf", 12.5, 14.6, 7);
                stations.Add(s1);
                stations.Add(s2);
                customers.Add(new Customer(325036551, "gfdx", "4545454", 12.3, 145.5));
                customers.Add(new Customer(325036552, "gfdx", "4545454", 12.3, 145.5));
                customers.Add(new Customer (325036553, "gfdx", "4545454", 12.3, 145.5));
                customers.Add(new Customer (325036554, "gfdx", "4545454", 12.3, 145.5));
                customers.Add( new Customer(325036555, "gfdx", "4545454", 12.3, 145.5));
                customers.Add (new Customer(325036556, "gfdx", "4545454", 12.3, 145.5));
                customers.Add (new Customer(325036557, "gfdx", "4545454", 12.3, 145.5));
                customers.Add( new Customer(325036558, "gfdx", "4545454", 12.3, 145.5));
                customers.Add (new Customer(325036559, "gfdx", "4545454", 12.3, 145.5));
                customers.Add (new Customer(325036550, "gfdx", "4545454", 12.3, 145.5));
                
                DateTime t = DateTime.Now;
                parcels.Add(new Parcel(325036550, 325036551, WeightCategories.Liver, Priorities.Regular,DateTime.Now,1, DateTime.Now, null,null));
                parcels.Add(new Parcel (325036559, 325036552, WeightCategories.Easy, Priorities.Regular, DateTime.Now, 1, null, null, null));
                parcels.Add(new Parcel(325036557, 325036558, WeightCategories.Intermediate, Priorities.Fast,DateTime.Now, 5, null, null, null));
                parcels.Add(new Parcel(325036556, 325036555, WeightCategories.Liver, Priorities.Fast, DateTime.Now, 5,null, null, null));
                parcels.Add(new Parcel(325036553, 325036550, WeightCategories.Easy, Priorities.Emergency, DateTime.Now, 1, null, null, null));
                parcels.Add(new Parcel(325036552, 325036559, WeightCategories.Intermediate, Priorities.Emergency,new DateTime(2021,05,5,9,23,12),4,new DateTime(2022,5,8,9,23,12),new DateTime(2022,5,15,9,23,12),new DateTime(2022,5,13,9,23,12)));
                parcels.Add(new Parcel(325036554, 325036557, WeightCategories.Liver, Priorities.Regular,new DateTime(2022,8,5,9,23,12),3,new DateTime(2022,5,5,9,23,12),null, null));
                parcels.Add(new Parcel(325036555, 325036551, WeightCategories.Liver, Priorities.Regular,new DateTime(2022,4,5,9,13,12),5,new DateTime(2022,5,9,9,23,12),new DateTime(2022,5,15,9,23,12),new DateTime(2022,5,25,9,23,12)));
                parcels.Add(new Parcel(325036553, 325036550, WeightCategories.Easy, Priorities.Emergency,new DateTime(2022,5,5,9,23,12),1,new DateTime(2022,5,5,9,23,12),new DateTime(2022,5,5,19,23,12), null));
                parcels.Add(new Parcel(325036559, 325036554, WeightCategories.Easy, Priorities.Emergency,DateTime.Now,1,new DateTime(2022,4,5,9,23,12),new DateTime(2022,4,8,9,23,12), null));
                parcels.Add(new Parcel(325036552, 325036551, WeightCategories.Liver, Priorities.Fast, DateTime.Now, 3, null, null, null));
                parcels.Add(new Parcel(325036556, 325036552, WeightCategories.Intermediate, Priorities.Fast,DateTime.Now,2, null, null, null));
            }
        }

    }

