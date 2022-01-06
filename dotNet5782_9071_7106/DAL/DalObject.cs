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

        public void AddStation(Station s)
        {
            foreach (var item in DataSource.stations)
            {
                if (item.Id == s.Id)
                    throw new IdException("already exist in the system");
            }
            DataSource.stations.Add(s);
        }
        public void AddDrone(Drone d)
        {
            foreach (var item in DataSource.drones)
            {
                if (item.Id == d.Id)
                    throw new IdException("already exist in the system");
            }
            DataSource.drones.Add(d);
        }

        public void AddCustomer(Customer c)
        {
            foreach (var item in DataSource.customers)
            {
                if (item.Id == c.Id)
                    throw new IdException("already exist in the system");
            }
            DataSource.customers.Add(c);
        }


        public int AddParcel(Parcel p)
        {
            DataSource.parcels.Add(p);
            return Parcel.endParcel;
        }
        public Station GetStation(int id)
        {
            for (int i = 0; i < DataSource.stations.Count; i++)
            {
                if (DataSource.stations[i].Id == id)
                    return DataSource.stations[i];
            }
            throw new NotExistException("this id not exist in the system");

        }
        public DroneCharge GetDroneCharge(int idDrone)
        {
            for (int i = 0; i < DataSource.DroneCharges.Count; i++)
            {
                if (DataSource.DroneCharges[i].Droneld == idDrone)
                    return DataSource.DroneCharges[i];
            }
            throw new NotExistException("this id not exist in the system");
        }
        public Drone GetDrone(int id)
        {
            for (int i = 0; i < DataSource.drones.Count; i++)
            {
                if (DataSource.drones[i].Id == id)
                    return DataSource.drones[i];
            }
            throw new NotExistException("this id not exist in the system");
        }
        public Customer GetCustomer(int id)
        {
            
            for (int i = 0; i < DataSource.customers.Count; i++)
            {
                if (DataSource.customers[i].Id == id) { 
                    return DataSource.customers[i];
                    
                }
            }
            
            throw new NotExistException("this id not exist in the system");
        }
        public Parcel GetParcel(int id)
        {
            for (int i = 0; i < DataSource.parcels.Count; i++)
            {
                if (DataSource.parcels[i].Id == id)
                    return DataSource.parcels[i];
            }
            throw new NotExistException("this id not exist in the system");
        }
        public IEnumerable<Station> GetListStation()
        {
            return new List<Station>(DataSource.stations);
        }
        public IEnumerable<Drone> GetListDrone()
        {

            return new List<Drone>(DataSource.drones);
        }
        public IEnumerable<Customer> getCustomer()
        {
            return new List<Customer>(DataSource.customers);
        }
        public IEnumerable<Parcel> getParcel()
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
        public IEnumerable<DroneCharge> GetListDroneCharges()
        {
            return new List<DroneCharge>(DataSource.DroneCharges);
        }
        public void UpdateParcelToDrone(int idDrone, int idParcel)
        {
            bool b = true;
            bool b2 = false;
            foreach (var item in DataSource.drones)
            {
                if (item.Id != idDrone)
                    b = false;
                else
                    b = true;
            }
            if (b == false)
            {
                throw new NotExistException("this id Drone not exist in the system");
            }
            for (int i = 0; i < DataSource.parcels.Count; i++)
            {
                if (DataSource.parcels[i].Id == idParcel)
                {
                    b2 = true;
                    var theParcel = DataSource.parcels[i];
                    theParcel.DroneId = idDrone;
                    DataSource.parcels[i] = theParcel;
                }
            }
            if (b2 == false)
            {
                throw new NotExistException("this id parcel not exist in the system");
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
                else
                {
                    throw new NotExistException("this id Drone not exist in the system");
                }
            }
        }
        public void targetId(int idCustomer, int idParcel)
        {
            bool c = true;
            bool c2 = false;
            foreach (var customer in DataSource.customers)
            {
                if (customer.Id != idCustomer)
                {
                    c = false;
                }
                else
                {
                    c = true;
                }
            }
            if (c == false)
            {
                throw new NotExistException("this id customer not exist in the system");
            }
            for (int i = 0; i < 1000 && DataSource.parcels != null; i++)
            {
                if (DataSource.parcels[i].Id == idParcel)
                {
                    c2 = true;
                    var theParcel = DataSource.parcels[i];
                    theParcel.TargetId = idCustomer;
                    DataSource.parcels[i] = theParcel;
                }
            }
            if (c2 == false)
            {
                throw new NotExistException("this id parcel not exist in the system");
            }
        }
        public void SendDroneToStation(int idDrone, int idStation)
        {
            bool s = true;
            bool s2 = false;
            foreach (var station in DataSource.stations)
            {
                if (station.Id != idStation)
                {//אם לא מצאת תחנה
                    s = false;
                }
                else//אם מצאת
                {
                    s = true;
                }
            }
            if (s == false)//:אם לא מצאת תחנה, זרוק
            {
                throw new NotExistException("this id station not exist in the system");
            }//אחרת
            for (int i = 0; i < DataSource.drones.Count; i++)
            {//תעבור על רשימת הרחפנים
                if (DataSource.drones[i].Id == idDrone)
                {//אם תמצא כזה רחפן
                    s2 = true;
                    var theDrone = DataSource.drones[i];
                    //תשמור במשתנה זה את הרחפן שמצאת
                    DataSource.drones[i] = theDrone;
                    DroneCharge a = new DroneCharge(idDrone, idStation);//מגדיר משתנה חדש שמקבל את הערכים שקיבלנו כקלט
                    DataSource.DroneCharges.Add(a);//מוסיף את המשתנה החדש
                }
            }
            if (s2 == false)
            {//:אם לא מצאת רחפן, זרוק
                throw new NotExistException("this id drone not exist in the system");
            }
        }
        public void FreeDrone(int idDrone)
        {
            bool f = false;
            for (int i = 0; i < DataSource.DroneCharges.Count; i++)
            {
                if (DataSource.DroneCharges[i].Droneld == idDrone)
                {
                    f = true;
                    DataSource.DroneCharges.Remove(DataSource.DroneCharges[i]);
                }
            }
            if (f == false)
            {
                throw new NotExistException("this id drone not exist in the system");
            }
        }
        public void removeFromDroneCharges(int idDrone,int idStation)
        {
            var d=DataSource.drones.FirstOrDefault(x => x.Id == idDrone);
            var s=DataSource.stations.FirstOrDefault(x => x.Id == idStation);
            DroneCharge dd = new DroneCharge(idDrone, idStation);
            if (!d.Equals(default) && !s.Equals(default))
            {
                GetListDroneCharges().ToList().Remove(dd);
            }
            else
            {
                throw new NotExistException("this drons'id or Station'id not exist in the system");
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
        public void UpdateParcel(Parcel p)
        {

            int index = getParcel().ToList().FindIndex(x => x.Id == p.Id);
            DataSource.parcels[index] = p;
        }
    }
}
