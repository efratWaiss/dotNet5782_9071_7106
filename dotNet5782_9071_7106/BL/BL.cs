using System;
using System.Collections.Generic;
using System.Linq;
using DalApi;
using IBL.BO;

#pragma warning disable IDE0005 // Using directive is unnecessary.
#pragma warning restore IDE0005 // Using directive is unnecessary.
#pragma warning disable IDE0005 // Using directive is unnecessary.
#pragma warning restore IDE0005 // Using directive is unnecessary.
namespace BlApi

{
    sealed partial class BL : IBL
    {
        
        static readonly BL instance = new BL();
        static BL() { }

        public static BL Instance { get => instance; }

       
        IDAL dal ;
        public List<DroneToList> DronesList = new();

        private double available;//רחפן ריק
        private double lightWeight;//קל
        public double MediumWeight { get; }//מדיום
        private double heavyWeight;//כבד
        private double ChargingRate;

        public BL()
        {
            //dal = new DalObject.DalObject();
            double[] arr;
            arr = dal.powerConsumpitionByDrone();
            available = arr[0];
            lightWeight = arr[1];
            MediumWeight = arr[2];
            heavyWeight = arr[3];
            ChargingRate = arr[4];
            dal = DalFactory.GetDAL("DalObjetc");
            Initialize();

        }

        private void Initialize()
        {
            DroneToList d;
            DroneStatuses st = new DroneStatuses();
            double battery = new double();
            Location lo = new Location(454.5, 45.5);

            foreach (var drone in dal.GetListDrone())
            {
                foreach (var parcel in dal.GetListParcel())
                {
                    if (parcel.DroneId == drone.Id && parcel.Delivered != null)
                    {

                        var customer = dal.GetListCustomer().FirstOrDefault(x => x.Id == parcel.SenderId);
                        st = DroneStatuses.Shipping;
                        if (parcel.scheduled != null && parcel.PickedUp == null)
                        {

                            double min = double.MaxValue;
                            foreach (var station in dal.GetListStation())
                            {
                                if (GetDistanceBetweenTwoLocation(new Location(station.Latitude, station.Longitude), new Location(customer.Latitude, customer.Longitude)) < min)
                                {
                                    min = GetDistanceBetweenTwoLocation(new Location(station.Latitude, station.Longitude), new Location(customer.Latitude, customer.Longitude));
                                    lo.Latitude = station.Latitude;
                                    lo.Longitude = station.Longitude;
                                }
                            }
                        }
                        else if (parcel.scheduled != null && parcel.PickedUp != null)
                        {
                            lo.Latitude = customer.Latitude;
                            lo.Longitude = customer.Longitude;
                        }

                        double tempLocation =
                                GetDistanceBetweenTwoLocation(new Location(dal.GetCustomer(parcel.SenderId).Longitude, dal.GetCustomer(parcel.SenderId).Latitude), DronesList.FirstOrDefault(x => x.Id == drone.Id).LocationNow)//מרחק בין מיקום רחפן לשולח
                                + GetDistanceBetweenTwoLocation(new Location(dal.GetCustomer(parcel.SenderId).Longitude, dal.GetCustomer(parcel.SenderId).Latitude), new Location(dal.GetCustomer(parcel.TargetId).Longitude, dal.GetCustomer(parcel.TargetId).Latitude))//מרחק בין שולח למקבל
                              + GetDistanceBetweenTwoLocation(new Location(dal.GetCustomer(parcel.TargetId).Longitude, dal.GetCustomer(parcel.TargetId).Latitude), new Location(lo.Longitude, lo.Latitude));
                        Random rn1 = new Random();
                        battery = rn1.Next((int)(tempLocation * ChargingRate * available), 100);
                        d = new DroneToList(drone.Id, drone.Model, (WeightCategories)drone.MaxWeight, battery, st, lo, parcel.Id);
                        DronesList.Add(d);
                    }

                    else
                    {

                        Random rn = new Random();
                        int rand = rn.Next(1, 3);
                        List<int> IDs = new List<int>();
                        switch (rand)
                        {
                            case 1:
                                int rr = rn.Next(1, dal.GetListStation().Count());
                                lo.Latitude = dal.GetListStation().ToList()[rr].Latitude;
                                lo.Longitude = dal.GetListStation().ToList()[rr].Longitude;
                                d = new DroneToList(drone.Id
                                    , drone.Model
                                    , (WeightCategories)drone.MaxWeight
                                    , rn.Next(0, 21)
                                    , DroneStatuses.Maintenance
                                    , lo
                                    , parcel.Id);
                                DronesList.Add(d);
                                break;
                            case 2:
                                foreach (var Customer in dal.GetListCustomer())
                                {
                                    foreach (var Parcel in dal.GetListParcel())
                                    {
                                        if (Customer.Id == Parcel.TargetId)
                                        {
                                            IDs.Add(Customer.Id);//מכניס את הת''ז של הלקוחות שסופקו להם חבילות
                                        }
                                    }

                                }
                                int p = IDs.Count();
                                rand = rn.Next(1, p);
                                foreach (var Customer in dal.GetListCustomer())
                                {
                                    if (Customer.Id == IDs[rand])
                                    {
                                        lo.Latitude = Customer.Latitude;
                                        lo.Longitude = Customer.Longitude;//מיקום הרחפן יהיה כמיקום הלקוח שהוגרל
                                    }
                                }
                                Location stationLocGet = new Location(12.7, 7.9);//איתחול משתנה בהמשך הפונקציה
                                double min = double.MaxValue;
                                foreach (var station in dal.GetListStation())
                                {
                                    if (GetDistanceBetweenTwoLocation(new Location(station.Latitude, station.Longitude), new Location(lo.Longitude, lo.Latitude)) < min)
                                    {
                                        min = GetDistanceBetweenTwoLocation(new Location(station.Latitude, station.Longitude), new Location(lo.Longitude, lo.Latitude));
                                        stationLocGet.Latitude = station.Latitude;
                                        stationLocGet.Longitude = station.Longitude;
                                    }
                                }
                                Random rn1 = new Random();
                                battery = rn1.Next((int)(min * ChargingRate * available), 100);
                                d = new DroneToList(drone.Id
                                    , drone.Model
                                    , (WeightCategories)drone.MaxWeight
                                    , battery
                                    , DroneStatuses.Vacant
                                    , stationLocGet
                                    , parcel.Id);
                                DronesList.Add(d);
                                break;
                        }
                    }
                    break;
                }
            }

        }
        public double GetDistanceBetweenTwoLocation(Location l1, Location l2)
        {

            return Math.Sqrt(Math.Pow(l1.Latitude - l2.Latitude, 2) + Math.Pow(l1.Longitude - l2.Longitude, 2));
        }
        private double droneWeight(int idDrone)
        {
            var drone = DronesList.FirstOrDefault(x => x.Id == idDrone);
            double wightDrone = 0;
            switch (drone.MaxWeight)
            {
                case WeightCategories.Easy:
                    wightDrone = lightWeight;
                    break;
                case WeightCategories.Intermediate:
                    wightDrone = MediumWeight;
                    break;
                case WeightCategories.Liver:
                    wightDrone = heavyWeight;
                    break;
                default:
                    wightDrone = available;
                    break;
            }
            return wightDrone;
        }
        
    }
}


