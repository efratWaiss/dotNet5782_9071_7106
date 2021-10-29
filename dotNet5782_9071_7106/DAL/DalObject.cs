using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
   namespace DalObject
    {
        public class DalObject
        {
            public DalObject()
            {
                DataSource.Initialize();

            }
            public void addStation(Station s)
            {
                DataSource.stations.Add(s);
            }
            public void addDrone(Drone d)
            {
                DataSource.drones.Add(d);
            }
            public void addCustomer(Customer c)
            {
                DataSource.customers.Add(c);
            }
            public int addParcel(Parcel p)
            {
                DataSource.parcels.Add(p);
                return Parcel.endParcel;
            }
            public void printStation(int id)
            {
                for(int i=0;i< 5&&DataSource.stations!=null;i++)
                {
                    if (DataSource.stations[i].Id == id)
                        DataSource.stations[i].ToString();
                }
            }

            public void printDrone(int id)
            {
                for (int i = 0; i < 10 && DataSource.drones != null; i++)
                {
                    if (DataSource.drones[i].Id == id)
                        DataSource.drones[i].ToString();
                }
            }
            public void printCustomer(int id)
            {
                for (int i = 0; i < 100 && DataSource.customers != null; i++)
                {
                    if (DataSource.customers[i].Id == id)
                        DataSource.customers[i].ToString();
                }
            }
            public void printParcel(int id)
            {
                for (int i = 0; i < 1000 && DataSource.parcels != null; i++)
                {
                    if (DataSource.parcels[i].Id == id)
                        DataSource.parcels[i].ToString();
                }
            }
            public void viewStation()
            {
                for(int i = 0; i < 5 && DataSource.stations != null; i++)
                {
                    Console.WriteLine(" Name: "+ DataSource.stations[i].Name);
                }       
            }
            public void viewDrone()
            {
                for (int i = 0; i < 10 && DataSource.drones != null; i++)
                {
                    Console.WriteLine(" Id: " + DataSource.drones[i].Id);
                }
            }
            public void viewCustomer(){
               for(int i = 0; i< 100 && DataSource.customers != null; i++)
                {
                    Console.WriteLine(" Name: " + DataSource.customers[i].Name);
                }
             }
            public void viewParcel()
            {
                for (int i = 0; i < 1000 && DataSource.parcels != null; i++)
                {
                    Console.WriteLine(" Id: " + DataSource.parcels[i].Id);
                }
            }

        }
    }
