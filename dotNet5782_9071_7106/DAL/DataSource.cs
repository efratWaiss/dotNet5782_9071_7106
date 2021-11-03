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
            internal static List<Drone>  drones=new List<Drone>();
            internal static List<Station>  stations=new List<Station>() ;
            internal static List<Customer>  customers=new List<Customer>() ;
            internal static List<Parcel>  parcels=new List<Parcel>() ;
            internal static List<DroneCharge> DroneCharges = new List<DroneCharge>();



            internal static void Initialize()
            {
                Drone d1=new Drone(1,"xyz122",WeightCategories.Easy,DroneStatuses.Shipping,50.3);
                Drone d2=new Drone(2,"xyz122",WeightCategories.Intermediate,DroneStatuses.Vacant,32.1);
                Drone d3=new Drone(2,"xyz122",WeightCategories.Intermediate,DroneStatuses.Maintenance,32.1);
                Drone d4=new Drone(2,"xyz122",WeightCategories.Intermediate,DroneStatuses.Vacant,32.1);
                Drone d5=new Drone(2,"xyz122",WeightCategories.Intermediate,DroneStatuses.Shipping,32.1);
                drones.Add(d1); 
                drones.Add(d2); 
                drones.Add(d3); 
                drones.Add(d4); 
                drones.Add(d5);
                Station s1 = new Station(54, "sfsf", 12.5, 14.6, 7);
                Station s2 = new Station(54, "sfsf", 12.5, 14.6, 7);
                stations.Add(s1);
                stations.Add(s2);
                Customer c1 = new Customer(325036555, "gfdx", "4545454", 12.3, 145.5);
                Customer c2 = new Customer(325036555, "gfdx", "4545454", 12.3, 145.5);
                Customer c3 = new Customer(325036555, "gfdx", "4545454", 12.3, 145.5);
                Customer c4 = new Customer(325036555, "gfdx", "4545454", 12.3, 145.5);
                Customer c5 = new Customer(325036555, "gfdx", "4545454", 12.3, 145.5);
                Customer c6 = new Customer(325036555, "gfdx", "4545454", 12.3, 145.5);
                Customer c7 = new Customer(325036555, "gfdx", "4545454", 12.3, 145.5);
                Customer c8 = new Customer(325036555, "gfdx", "4545454", 12.3, 145.5);
                Customer c9 = new Customer(325036555, "gfdx", "4545454", 12.3, 145.5);
                Customer c10 = new Customer(325036555, "gfdx", "4545454", 12.3, 145.5);
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
                Parcel p1 = new Parcel(1, 12, 5, WeightCategories.Liver, Priorities.Regular, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, 8);
                Parcel p2 = new Parcel(2, 12, 5, WeightCategories.Liver, Priorities.Fast, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, 8);
                Parcel p3 = new Parcel(3, 12, 5, WeightCategories.Liver, Priorities.Emergency, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, 8);
                Parcel p4 = new Parcel(4, 12, 5, WeightCategories.Liver, Priorities.Regular, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, 8);
                Parcel p5 = new Parcel(5, 12, 5, WeightCategories.Liver, Priorities.Fast, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, 8);
                Parcel p6 = new Parcel(6, 12, 5, WeightCategories.Liver, Priorities.Regular, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, 8);
                Parcel p7 = new Parcel(7, 12, 5, WeightCategories.Liver, Priorities.Emergency, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, 8);
                Parcel p8 = new Parcel(8, 12, 5, WeightCategories.Liver, Priorities.Regular, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, 8);
                Parcel p9 = new Parcel(9, 12, 5, WeightCategories.Liver, Priorities.Fast, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, 8);
                Parcel p10 = new Parcel(10, 12, 5, WeightCategories.Liver, Priorities.Regular, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, 8);









            }
        }

    }
}
