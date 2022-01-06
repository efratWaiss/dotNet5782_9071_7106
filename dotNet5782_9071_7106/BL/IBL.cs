using IBL.BO;
using System;
using System.Collections.Generic;

namespace IBL
{
    interface IBL
    {
        public void addStation(int Id ,String Name,Location location,int AvailableChargeSlots);
        public void addDrone(int Id ,String Model,WeightCategories Weight,int IdStation);
        public void addCustomer(int Id ,String Name,String Phone,double Longitude,double Latitude);

        public int addParcel(int SenderId,int TargetId, WeightCategories Weight,Priorities Priorities);
        public Station printStation(int id);
        public Drone printDrone(int id);
        public Customer printCustomer(int id);
        public Parcel printParcel(int id);
        public IEnumerable<StationToList> getStation();
        public IEnumerable<DroneToList> getDrone();
        public IEnumerable<CustomerToList> getCustomer();
        public IEnumerable<ParcelToList> getParcel();
        public IEnumerable<ParcelToList> ParcelNoDrone();
        public IEnumerable<StationToList> stationWithAvailableStands();
        public void updateParcelToDrone(int idDrone);
        public void DeliveryOfAParcelByDrone(int idDrone);
        public void PackageCollectionByDrone(int idDrone);
        public void sendDroneToStation(int idDrone);
        public void freeDrone(int idDrone,double timeInCharging);
  
    }
}
