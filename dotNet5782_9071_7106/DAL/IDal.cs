using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace IDal
{
    interface IDal
    {

        public void addStation(Station s);
        public void addDrone(Drone d);
        public void addCustomer(Customer c);

        public int addParcel(Parcel p);
        public Station printStation(int id);
        public Drone printDrone(int id);
        public Customer printCustomer(int id);
        public Parcel printParcel(int id);
        public IEnumerable<Station> viewStation();
        public IEnumerable<Drone> viewDrone();
        public IEnumerable<Customer> viewCustomer();
        public IEnumerable<Parcel> viewParcel();
        public IEnumerable<Parcel> ParcelNoDrone();
        public IEnumerable<Station> StationNoCharge();
        public void updateParcelToDrone(int idDrone, int idParcel);
        public void pickedUpD(int idDrone, DateTime d);
        public void targetId(int idCustomer, int idParcel);
        public void sendDroneToStation(int idDrone, int idStation);
        public void freeDrone(int idDrone);
        public double[] powerConsumpitionByDrone(); 
    }
}
