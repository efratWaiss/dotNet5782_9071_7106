using BO;
//<<<<<<< HEAD
//using IBL.BO;
//=======
//>>>>>>> cbcf2b16fc2b8b9724be35762ed329278ca6d9cc
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BlApi
{
    public interface IBL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStation(int Id, String Name, Location location, int AvailableChargeSlots);
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(int Id, String Model, WeightCategories Weight, int IdStation);
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(int Id, String Name, String Phone, double Longitude, double Latitude);
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int AddParcel(int SenderId, int TargetId, WeightCategories Weight, Priorities Priorities);
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station GetStation(int id);
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(int id);
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(int id);
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetParcel(int id);
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationToList> GetListStation();
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneToList> GetListDrone();
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<CustomerToList> GetListCustomer();
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> GetListParcel();
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> ParcelNoDrone();
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationToList> StationWithAvailableStands();
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateParcelToDrone(int idDrone);
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomerDetails(int IdCustomer, String? Name, String? Phone);
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateNameDrone(int idDrone, string model);
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeliveryOfAParcelByDrone(int idDrone);
        public void CollectionAParcelByDroen(int idDrone);
        public void SendDroneToStation(int idDrone);//
        public void FreeDrone(int idDrone, double timeInCharging);
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneToList> GetListByStatus(DroneStatuses stasus);
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneToList> GetListByWeight(WeightCategories weight);
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateParcelDetails(int Id, BO.WeightCategories w, BO.Priorities p);
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateStationDetails(int IdStation, String NameStation, int ChargeSlots);
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<IGrouping<DroneStatuses, DroneToList>> GetListDroneByGroup();
        //public IEnumerable<IGrouping<int, ParcelToList>> GetListParceleByGroup();
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<IGrouping<int, StationToList>> GetListStationByGroup();
        //public IEnumerable<IGrouping<int, ParcelToList>> GetListParcelByGroup(BO.Customer c);
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<IGrouping<int, ParcelToList>> GetListParcelBySenderGroup();
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<IGrouping<int, ParcelToList>> GetListParcelByTargetGroup();
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> GetListParcelByDate(DateTime from, DateTime to);
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteParcel(int id);
    }
}
