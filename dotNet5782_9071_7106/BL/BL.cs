using System;
using System.Collections.Generic;
using IBL.BO;
#pragma warning disable IDE0005 // Using directive is unnecessary.
#pragma warning restore IDE0005 // Using directive is unnecessary.
#pragma warning disable IDE0005 // Using directive is unnecessary.
#pragma warning restore IDE0005 // Using directive is unnecessary.
namespace IBL

{
    class BL : IBL
    {
        IDAL.IDal dal=new DalObject.DalObject();
        private readonly List<Drone> dronesList;
        private double available;
        private double lightWeight;

        public double MediumWeight { get; }

        private double heavyWeight;

        public BL()
        {
            dal = new DalObject.DalObject();
            dronesList = new List<Drone>();
            double[] arr;
            arr = dal.GetPowerConsumptionByDrone();
            available = arr[0];
            lightWeight = arr[1];
            MediumWeight = arr[2];
            heavyWeight = arr[3];
            ChargingRate = arr[4];
            Initialize();
            powerConsumpitionByDrone();
        }
        
        private void Initialize()
        {

           // var getDrones=dal.
            throw new NotImplementedException();
        }

        public double CarryingHeavyWeight { get; }
        public double ChargingRate { get; }

        public void addCustomer(Customer c)
        {
            throw new NotImplementedException();
        }

        public void addDrone(Drone d)
        {
            throw new NotImplementedException();
        }

        public int addParcel(Parcel p)
        {
            throw new NotImplementedException();
        }

        public void addStation(Station s)
        {
            throw new NotImplementedException();
        }

        public void freeDrone(int idDrone)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Parcel> ParcelNoDrone()
        {
            throw new NotImplementedException();
        }

        public void pickedUpD(int idDrone, DateTime d)
        {
            throw new NotImplementedException();
        }

        public double[] powerConsumpitionByDrone()
        {
            throw new NotImplementedException();
        }

        public Customer printCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public Drone printDrone(int id)
        {
            throw new NotImplementedException();
        }

        public Parcel printParcel(int id)
        {
            throw new NotImplementedException();
        }

        public Station printStation(int id)
        {
            throw new NotImplementedException();
        }

        public void sendDroneToStation(int idDrone, int idStation)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> StationNoCharge()
        {
            throw new NotImplementedException();
        }

        public void targetId(int idCustomer, int idParcel)
        {
            throw new NotImplementedException();
        }

        public void updateParcelToDrone(int idDrone, int idParcel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> viewCustomer()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Drone> viewDrone()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Parcel> viewParcel()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> viewStation()
        {
            throw new NotImplementedException();
        }
    }
}

namespace IDAL
{
    class IDal
    {
        internal double[] GetPowerConsumptionByDrone()
        {
            throw new NotImplementedException();
        }
    }
}