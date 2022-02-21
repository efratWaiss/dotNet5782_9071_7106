using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DalApi
{
    public interface IDAL
    {

        public void AddStation(Station s);//v
        public void AddDrone(Drone d);//v
        public void AddCustomer(Customer c);//v
        public int AddParcel(Parcel p);//v
        public Station GetStation(int id);//v
        public Drone GetDrone(int id);//v
        public Customer GetCustomer(int id);//v
        public Parcel GetParcel(int id);//v
        public DroneCharge GetDroneCharge(int idDrone);//v
        public IEnumerable<DroneCharge> GetListDroneCharges();//v
        public IEnumerable<Station> GetListStation();//v
        public IEnumerable<Drone> GetListDrone();//v
        public IEnumerable<Customer> GetListCustomer();//v
        public IEnumerable<Parcel> GetListParcel();//v
        public IEnumerable<Parcel> ParcelNoDrone();//v
        public IEnumerable<Station> StationNoCharge();//v
        public void UpdateParcelToDrone(int idDrone, int idParcel);//v
        public void UpdateStationDetails(int IdStation, String? NameStation, int? ChargeSlots);//v
        public void UpdateCustomerDetails(int IdCustomer, String? Name, String? Phone);//v
        public void pickedUpD(int idDrone, DateTime d);
        public void targetId(int idCustomer, int idParcel);//v
        public void SendDroneToStation(int idDrone, int idStation);//v
        public void FreeDrone(int idDrone);//v
        public double[] powerConsumpitionByDrone();
        public void UpdateParcel(Parcel p);//v
        public void removeFromDroneCharges(int idDrone, int idStation);//v
        public void DealeteParcel(int id);//v
         }
}
