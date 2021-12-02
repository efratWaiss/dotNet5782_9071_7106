using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    public class DalObject : IDal.IDal
    {

        public DalObject()
        {
            DataSource.Initialize();
        }

        public void addStation(Station s)
        {
            foreach (var item in DataSource.stations)
            {
                if (item.Id == s.Id)
                    throw new idException("already exist in the system");
            }
            DataSource.stations.Add(s);
        }
        public void addDrone(Drone d)
        {
            foreach (var item in DataSource.drones)
            {
                if (item.Id == d.Id)
                    throw new idException("already exist in the system");
            }
            DataSource.drones.Add(d);
        }

        public void addCustomer(Customer c)
        {
            foreach (var item in DataSource.customers)
            {
                if (item.Id == c.Id)
                    throw new idException("already exist in the system");
            }
            DataSource.customers.Add(c);
        }


        public int addParcel(Parcel p)
        {
            DataSource.parcels.Add(p);
            return Parcel.endParcel;
        }
        public Station printStation(int id)
        {
            for (int i = 0; i < DataSource.stations.Count; i++)
            {
                if (DataSource.stations[i].Id == id)
                    return DataSource.stations[i];
            }
            throw new notExistException("this id not exist in the system");

        }

        public Drone printDrone(int id)
        {
            for (int i = 0; i < DataSource.drones.Count; i++)
            {
                if (DataSource.drones[i].Id == id)
                    return DataSource.drones[i];
            }
            throw new notExistException("this id not exist in the system");
        }
        public Customer printCustomer(int id)
        {
            for (int i = 0; i < DataSource.customers.Count; i++)
            {
                if (DataSource.customers[i].Id == id)
                    return DataSource.customers[i];
            }
            throw new notExistException("this id not exist in the system");
        }
        public Parcel printParcel(int id)
        {
            for (int i = 0; i < DataSource.parcels.Count; i++)
            {
                if (DataSource.parcels[i].Id == id)
                    return DataSource.parcels[i];
            }
            throw new notExistException("this id not exist in the system");
        }
        public IEnumerable<Station> viewStation()
        {
            return new List<Station>(DataSource.stations);
        }
        public IEnumerable<Drone> viewDrone()
        {

            return new List<Drone>(DataSource.drones);
        }
        public IEnumerable<Customer> viewCustomer()
        {
            return new List<Customer>(DataSource.customers);
        }
        public IEnumerable<Parcel> viewParcel()
        {
            return new List<Parcel>(DataSource.parcels);
        }
        public IEnumerable<Parcel> ParcelNoDrone()
        {
            List<Parcel> ll = new List<Parcel>();
            foreach (var item in DataSource.parcels)
            {
                if (item.DroneId == 0)
                    ll.Add(item);
            }

            return ll;
        }
        public IEnumerable<Station> StationNoCharge()
        {
            List<Station> ll = new List<Station>();
            foreach (var item in DataSource.stations)
            {
                if (item.ChargeSlots == 0)
                    ll.Add(item);
            }

            return ll;
        }
        public void updateParcelToDrone(int idDrone, int idParcel)//what the problem
        {
            bool b = true;
            bool b2=false;
            foreach(var item in DataSource.drones)
            {
                if (item.Id != idDrone)
                    b = false;
                else
                    b = true;
            }
            if (b == false)
            {
                throw new notExistException("this id Drone not exist in the system");
            }
            for (int i = 0; i < DataSource.parcels.Count; i++)
            {
                if (DataSource.parcels[i].Id == idParcel)
                {
                    b2=true;
                    var theParcel = DataSource.parcels[i];
                    theParcel.DroneId = idDrone;
                    DataSource.parcels[i] = theParcel;
                }
                 }
            if (b2 == false)
            {
                throw new notExistException("this id parcel not exist in the system");
            }
            
        }
        public void pickedUpD(int idDrone, DateTime d)
        {
            for (int i = 0; i < DataSource.parcels.Count; i++)
            {
                if (DataSource.parcels[i].DroneId == idDrone)
                {
                    var w = DataSource.parcels[i];
                    w.PickedUp = DateTime.Now;
                    DataSource.parcels[i] = w;
                }
                else { 
                throw new notExistException("this id Drone not exist in the system");
            }}
        }
        public void targetId(int idCustomer, int idParcel)
        {
            bool c=true;
            bool c2=false;
            foreach (var customer in DataSource.customers)
            {
                if (customer.Id != idCustomer)
                {
                    c=false;
                }
                else
                {
                    c=true;
                }
            }
            if (c == false)
            {
                throw new notExistException("this id customer not exist in the system");
            }
            for (int i = 0; i < 1000 && DataSource.parcels != null; i++)
            {
                if (DataSource.parcels[i].Id == idParcel)
                {
                    c2=true;
                    var theParcel = DataSource.parcels[i];
                    theParcel.TargetId = idCustomer;
                    DataSource.parcels[i] = theParcel;
                }
            }
            if (c2 == false)
            {
                 throw new notExistException("this id parcel not exist in the system");
            }
        }
        public void sendDroneToStation(int idDrone, int idStation)
        {
            bool s=true;
            bool s2=false;
             foreach (var station in DataSource.stations)
            {
                if (station.Id != idStation)
                {
                    s=false;
                }
                else
                {
                    s=true;
                }
            }
            if (s == false)
            {
                throw new notExistException("this id station not exist in the system");
            }
            for (int i = 0; i < DataSource.drones.Count; i++)
            {
                if (DataSource.drones[i].Id == idDrone)
                {
                    s2=true;
                    var theDrone = DataSource.drones[i];

                    DataSource.drones[i] = theDrone;
                    DroneCharge a = new DroneCharge(idDrone, idStation);
                    DataSource.DroneCharges.Add(a);
                }
            }
            if (s2 == false)
            {
                throw new notExistException("this id drone not exist in the system");
            }
        }
        public void freeDrone(int idDrone)
        {
            bool f=false;
            for (int i = 0; i < DataSource.DroneCharges.Count; i++)
            {
                if (DataSource.DroneCharges[i].Droneld == idDrone)
                {
                    f=true;
                    DataSource.DroneCharges.Remove(DataSource.DroneCharges[i]);
                }
            }
            if (f == false)
            {
                throw new notExistException("this id drone not exist in the system");
            }
        }

        public double[] powerConsumpitionByDrone()
        {

            double[] l = new double[5];
            l[0] = DataSource.Config.available;
            l[1] = DataSource.Config.lightWeight;
            l[2] = DataSource.Config.MediumWeight;
            l[3] = DataSource.Config.heavyWeight;
            l[4] = DataSource.Config.ChargingRate;

            return l;
        }
    }
}
