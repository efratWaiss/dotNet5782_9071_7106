using BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlApi
{
   public interface IBL
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
        public void UpdateNameDrone(int idDrone, string model);
        public void DeliveryOfAParcelByDrone(int idDrone);
        public void PackageCollectionByDrone(int idDrone);
        public void SendDroneToStation(int idDrone);
        public void FreeDrone(int idDrone,double timeInCharging);
        public IEnumerable<DroneToList> GetListByStatus(DroneStatuses stasus);
        public IEnumerable<DroneToList> GetListByWeight(WeightCategories weight);
        public IEnumerable<IGrouping<DroneStatuses, DroneToList>> GetListDroneByGroup();

    }
}
