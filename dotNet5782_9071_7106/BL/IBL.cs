using IBL.BO;
using System;
using System.Collections.Generic;

namespace IBL
{
    interface IBL
    {
        public void AddStation(int Id ,String Name,Location location,int AvailableChargeSlots);
        public void AddDrone(int Id ,String Model,WeightCategories Weight,int IdStation);
        public void AddCustomer(int Id ,String Name,String Phone,double Longitude,double Latitude);

        public int AddParcel(int SenderId,int TargetId, WeightCategories Weight,Priorities Priorities);
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
        public void DeliveryOfAParcelByDrone(int idDrone);
        public void PackageCollectionByDrone(int idDrone);
        public void SendDroneToStation(int idDrone);
        public void FreeDrone(int idDrone,double timeInCharging);
  
    }
}
