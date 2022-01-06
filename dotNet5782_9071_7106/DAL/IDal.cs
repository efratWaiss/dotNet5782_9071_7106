using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace IDal
{
    public interface IDal
    {

        public void AddStation(Station s);
        public void AddDrone(Drone d);
        public void AddCustomer(Customer c);

        public int AddParcel(Parcel p);
        public Station GetStation(int id);
        public Drone GetDrone(int id);
        public Customer GetCustomer(int id);
        public Parcel GetParcel(int id);
        public DroneCharge GetDroneCharge(int idDrone);
        public IEnumerable<DroneCharge> GetListDroneCharges();
        public IEnumerable<Station> GetListStation();
        public IEnumerable<Drone> GetListDrone();
        public IEnumerable<Customer> GetListCustomer();
        public IEnumerable<Parcel> GetListParcel();
        public IEnumerable<Parcel> ParcelNoDrone();
        public IEnumerable<Station> StationNoCharge();
        public void UpdateParcelToDrone(int idDrone, int idParcel);
        public void pickedUpD(int idDrone, DateTime d);
        public void targetId(int idCustomer, int idParcel);
        public void SendDroneToStation(int idDrone, int idStation);
        public void FreeDrone(int idDrone);
        public double[] powerConsumpitionByDrone();
        public void UpdateParcel(Parcel p);
        public void removeFromDroneCharges(int idDrone, int idStation);
    }
}
