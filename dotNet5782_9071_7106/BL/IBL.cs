using BO;
//<<<<<<< HEAD
//using IBL.BO;
//=======
//>>>>>>> cbcf2b16fc2b8b9724be35762ed329278ca6d9cc
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlApi
{
    public interface IBL
    {
        public void AddStation(int Id, String Name, Location location, int AvailableChargeSlots);
        public void AddDrone(int Id, String Model, WeightCategories Weight, int IdStation);
        public void AddCustomer(int Id, String Name, String Phone, double Longitude, double Latitude);
        public int AddParcel(int SenderId, int TargetId, WeightCategories Weight, Priorities Priorities);
        public Station GetStation(int id);
        public Drone GetDrone(int id);
        public Customer GetCustomer(int id);
        public Parcel GetParcel(int id);
        public IEnumerable<StationToList> GetListStation();
        public IEnumerable<DroneToList> GetListDrone();
        public IEnumerable<CustomerToList> GetListCustomer();
        public IEnumerable<ParcelToList> GetListParcel();
        public IEnumerable<ParcelToList> ParcelNoDrone();
        public IEnumerable<StationToList> StationWithAvailableStands();
        public void UpdateParcelToDrone(int idDrone);
        public void UpdateCustomerDetails(int IdCustomer, String? Name, String? Phone);
        public void UpdateNameDrone(int idDrone, string model);
        public void DeliveryOfAParcelByDrone(int idDrone);
        public void PackageCollectionByDrone(int idDrone);
        public void SendDroneToStation(int idDrone);
        public void FreeDrone(int idDrone, double timeInCharging);
        public IEnumerable<DroneToList> GetListByStatus(DroneStatuses stasus);
        public IEnumerable<DroneToList> GetListByWeight(WeightCategories weight);
        public void UpdateStationDetails(int v1, string v2, double v3);
        public IEnumerable<IGrouping<DroneStatuses, DroneToList>> GetListDroneByGroup();
        //public IEnumerable<IGrouping<int, ParcelToList>> GetListParceleByGroup();
        public IEnumerable<IGrouping<int, StationToList>> GetListStationByGroup();
        //public IEnumerable<IGrouping<int, ParcelToList>> GetListParcelByGroup(BO.Customer c);
        public IEnumerable<IGrouping<int, ParcelToList>> GetListParcelBySenderGroup();
        public IEnumerable<IGrouping<int, ParcelToList>> GetListParcelByTargetGroup();
        public IEnumerable<ParcelToList> GetListParcelByDate(DateTime from, DateTime to);
        public void DeleteParcel(int id);
    }
}
