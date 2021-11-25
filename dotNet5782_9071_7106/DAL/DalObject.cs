using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
   namespace DalObject
    {
        public class DalObject:IDAL 
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
            public Station printStation(int id)
            {
                for(int i=0;i< DataSource.stations.Count;i++)
                {
                    if (DataSource.stations[i].Id == id)
                       return DataSource.stations[i];
                }
           return  default (Station);
            }

            public Drone printDrone(int id)
            {
                for (int i = 0; i < DataSource.drones.Count; i++)
                {
                    if (DataSource.drones[i].Id == id)
                       return DataSource.drones[i];
                }
            return default(Drone);
        }
            public Customer printCustomer(int id)
            {
                for (int i = 0; i<DataSource.customers.Count; i++)
                {
                    if (DataSource.customers[i].Id == id)
                      return  DataSource.customers[i];
                }
            return default(Customer);
        }
            public Parcel printParcel(int id)
            {
                for (int i = 0; i <DataSource.parcels.Count; i++)
                {
                    if (DataSource.parcels[i].Id == id)
                       return DataSource.parcels[i];
                }
            return default(Parcel);
        }
            public IEnumerable viewStation()
            {
                IEnumerable list = DataSource.stations;
                return list;
            }
            public List<Drone> viewDrone()
            {
            return DataSource.drones;
        }
            public List<Customer> viewCustomer(){
            return DataSource.customers;
        }
            public List<Parcel> viewParcel()
            {
            return DataSource.parcels;
        }
         public void updateParcelToDrone(int idDrone,int idParcel)//what the problem
         {
           for (int i=0;i<DataSource.parcels.Count; i++)
           {
             if (DataSource.parcels[i].Id == idParcel) 
             {
                    var theParcel = DataSource.parcels[i];
                    theParcel.DroneId = idDrone;
                    DataSource.parcels[i] = theParcel;
             }
           }
         }
     public void pickedUpD(int idDrone, DateTime d)
     {
         for (int i = 0; i <DataSource.parcels.Count; i++)
         {
             if (DataSource.parcels[i].DroneId == idDrone)
             {
                    var w = DataSource.parcels[i];
                    w.PickedUp = DateTime.Now;
                    DataSource.parcels[i] = w;
                }
         }
     }
     public void targetId(int idCustomer, int idParcel)
     {
         for (int i = 0; i < 1000 && DataSource.parcels != null; i++)
         {
             if (DataSource.parcels[i].Id== idParcel)
             {
                    var theParcel = DataSource.parcels[i];
                    theParcel.TargetId = idCustomer;
                    DataSource.parcels[i] = theParcel;
             }
         }
     }
        public void sendDroneToStation(int idDrone,int idStation)
        {
            for (int i = 0; i < DataSource.drones.Count; i++)
            {
                if (DataSource.drones[i].Id == idDrone)
                {
                    var theDrone = DataSource.drones[i];
                    
                    DataSource.drones[i] = theDrone;
                    DroneCharge a = new DroneCharge(idDrone,idStation);
                    DataSource.DroneCharges.Add(a);
                }
            }
        }
        public void freeDrone(int idDrone)
        {
            for (int i = 0; i < DataSource.DroneCharges.Count; i++)
            {
                if (DataSource.DroneCharges[i].Droneld == idDrone)
                {
                    DataSource.DroneCharges.Remove(DataSource.DroneCharges[i]);
                }
                }
            }
        public double [] powerConsumpitionByDrone()
        {

        }
    }
}
