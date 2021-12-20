using System;
using System.Collections.Generic;
using System.Linq;
using IBL.BO;
#pragma warning disable IDE0005 // Using directive is unnecessary.
#pragma warning restore IDE0005 // Using directive is unnecessary.
#pragma warning disable IDE0005 // Using directive is unnecessary.
#pragma warning restore IDE0005 // Using directive is unnecessary.
namespace IBL

{
    class BL : IBL
    {
        IDal.IDal dal = new DalObject.DalObject();
        private readonly List<Drone> DronesList = new();
        private List<Customer> CustomersList = new();
        private List<Parcel> ParcelsList = new();
        private List<Station> StationsList = new();
        private double available;
        private double lightWeight;


        public double MediumWeight { get; }

        private double heavyWeight;

        public BL()
        {
            dal = new DalObject.DalObject();
            DronesList = new List<Drone>();
            double[] arr;
            arr = dal.powerConsumpitionByDrone();
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
            var parcelsDal = dal.viewParcel();
            var DronesDal = dal.viewDrone();
            var CustomersDal = dal.viewCustomer();
            var StationsDal = dal.viewStation().ToList();

            DroneStatuses st = new DroneStatuses();
            double battery = new double();
            Location lo = new Location(454.5, 45.5);

            foreach (var drone in DronesDal)
            {
                foreach (var parcel in parcelsDal)
                {
                    if (parcel.DroneId == drone.Id && parcel.Delivered != null)
                    {
                        // ParcelInTransference p = new ParcelInTransference(parcel.Id, parcel.SenderId, parcel.TargetId, parcel.Weight, parcel.priority, true, null, null, null);

                        var customer = dal.viewCustomer().FirstOrDefault(x => x.Id == parcel.SenderId);
                        st = DroneStatuses.Shipping;
                        if (parcel.scheduled != null && parcel.PickedUp == null)
                        {

                            double min = double.MaxValue;
                            foreach (var station in StationsDal)
                            {
                                if (GetDistanceBetweenTwoLocation(new Location(station.Latitude, station.Longitude), new Location(customer.Latitude, customer.Longitude)) < min)
                                {
                                    min = GetDistanceBetweenTwoLocation(new Location(station.Latitude, station.Longitude), new Location(customer.Latitude, customer.Longitude));
                                    lo.Latitude = station.Latitude;
                                    lo.Longitude = station.Longitude;
                                }
                            }
                        }
                        if (parcel.PickedUp != null)
                        {
                            lo.Latitude = customer.Latitude;
                            lo.Longitude = customer.Longitude;
                        }
                        battery = 45.3;///לסדר את הבטרייה 
                        Drone d = new Drone(drone.Id, drone.Model, (WeightCategories)drone.MaxWeight, st, battery, null/*יש לשנות-להכניס parcel*/, lo);
                        DronesList.Add(d);
                    }


                }
                Random rn = new Random();
                int rand = rn.Next(1, 3);
                List<int> IDs = new List<int>();
                switch (rand)
                {
                    case 1:
                        int rr = rn.Next(1, StationsDal.Count());
                        lo.Latitude = StationsDal[rr].Latitude;
                        lo.Longitude = StationsDal[rr].Longitude;
                        Drone d = new Drone(drone.Id, drone.Model, (WeightCategories)drone.MaxWeight, DroneStatuses.Maintenance, rn.Next(0, 21), null, lo);
                        break;
                    case 2:
                        foreach (var Customer in CustomersDal)
                        {
                            foreach (var Parcel in parcelsDal)
                            {
                                if (Customer.Id == Parcel.TargetId)
                                {
                                    IDs.Add(Customer.Id);//מכניס את הת''ז של הלקוחות שסופקו להם חבילות
                                }
                            }

                        }
                        rand = rn.Next(1, IDs.Count());
                        foreach (var Customer in CustomersDal)
                        {
                            if (Customer.Id == IDs[rand])
                            {
                                lo.Latitude = Customer.Latitude;
                                lo.Longitude = Customer.Longitude;//מיקום הרחפן יהיה כמיקום הלקוח שהוגרל
                            }
                        }
                        d = new Drone(drone.Id, drone.Model, (WeightCategories)drone.MaxWeight, DroneStatuses.Vacant, battery, null, lo);
                        break;
                }



            }

            throw new NotImplementedException();
        }



        public double GetDistanceBetweenTwoLocation(Location l1, Location l2)
        {
            return Math.Sqrt(Math.Pow(l1.Latitude - l2.Latitude, 2) + Math.Pow(l1.Longitude - l2.Longitude, 2));
        }

        public double CarryingHeavyWeight { get; }
        public double ChargingRate { get; }

        public void addCustomer(Customer c)
        {
            var customer = dal.viewCustomer().FirstOrDefault(x => x.Id == c.Id);
            if (customer.Id != 0) { // :אם מצאת כבר לקוח כזה, זרוק 
                throw new NotImplementedException("already exist in the system"); }
            else
            {
                CustomersList.Add(c);
                // dal להכניס לרשימה מסוג  
                //dal.addCustomer(new IDAL.DO.Customer(c));
            }
        }

        public void addDrone(Drone d)
        {
            var drone = dal.viewDrone().FirstOrDefault(x => x.Id == d.Id);
            if (drone.Id != 0)
            { // :אם מצאת כבר רחפן כזה, זרוק 
                throw new NotImplementedException("already exist in the system");
            }
            else
            {
                DronesList.Add(d);
                // dal להכניס לרשימה מסוג  

            }
        }

        public int addParcel(Parcel p)
        {
            ParcelsList.Add(p);
            return Parcel.Id; }

        public void addStation(Station s)
        {
            var station = dal.viewDrone().FirstOrDefault(x => x.Id == s.Id);
            if (station.Id != 0)
            { // :אם מצאת כבר רחפן כזה, זרוק 
                throw new NotImplementedException("already exist in the system");
            }
            else
            {
                StationsList.Add(s);
                // dal להכניס לרשימה מסוג  

            }
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
            foreach (var customer in CustomersList)
            {
                if (customer.Id == id)
                {
                    return customer;
                }
            }

            throw new NotImplementedException("this id not exist in the system");
        }

    

        public Drone printDrone(int id)
        {
            foreach (var drone in DronesList)
            {
                if (drone.Id == id)
                {
                    return drone;
                }
            }

            throw new NotImplementedException("this id not exist in the system");
        }

        public Parcel printParcel(int id)
        {
            foreach (var parcel in ParcelsList)
            {
                if (parcel.ParcelId == id)
                {
                    return parcel;
                }
            }

            throw new NotImplementedException("this id not exist in the system");
        }

        public Station printStation(int id)
        {
            foreach (var station in StationsList)
            {
                if (station.Id == id)
                {
                    return station;
                }
            }
            
            throw new NotImplementedException("this id not exist in the system");
        }

        public void sendDroneToStation(int idDrone, int idStation)
        {
            bool check1 = false;
            bool check2 = false;
            int index = 0;
            Drone tempDrone=DronesList[0];//אתחול בערך זמני
            while (index != DronesList.Count() && check1 == false)
            {//כל עוד לא מצאת רחפן תואם לקלט וגם לא הגעת לסוף הרשימה
                if (DronesList[index].Id == idDrone)
                {//אם מצאת
                    check1 = true;//תשנה את משתנה "בדיקה1" לאמת, כדי שהלולאה תעצור
                    tempDrone = DronesList[index];//תשמור במשתנה עזר את הרחפן שמצאת
                }
                index++;//תקדם את האינדקס
            }
            if (check1 == false)
            {//:אם הגעת לסוף הרשימה בלי שמצאת רחפן מתאים, זרוק
                throw new NotImplementedException("this Drone's id not exist in the system");
            }

            foreach (var station in StationsList)
            {//מעבר על רשימת התחנות
                if (station.Id == idStation)
                {//,אם מצאת תחנה מתאימה לקלט שהתקבל, תשמור במשתנה עזר שמשייך ברשימה את הרחפן לתחנה את הת''ז של הרחפו שהתקבל בקלט,
                    DroneInCharging temp = new DroneInCharging(idDrone,tempDrone.battery);//ואת הבטריה של הרחפן התואם לקלט שנמצא בסעיף הקודם
                    station.DronesInCharging.Add(temp);//תכניס לרשימת הרחפנים בתחנה את המשתנה שמקשר לרחפן זה
                }
            }
            if (check2 == false)
            {
                throw new NotImplementedException("this Station's id not exist in the system");
            }

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
            return new List<Customer> (CustomersList);
        }

        public IEnumerable<Drone> viewDrone()
        {
            return new List<Drone> (DronesList);
        }

        public IEnumerable<Parcel> viewParcel()
        {
            return new List<Parcel>(ParcelsList);
        }

        public IEnumerable<Station> viewStation()
        {
          return new List<Station> (StationsList);
        }
        //public void ParcelsNotSend(IEnumerable<Parcel> parcels, List<Drone> drones, List<Station> stations)
        //{


        //}
    }
}
//namespace IDAL
//{
//    class IDal
//    {
//        internal double[] GetPowerConsumptionByDrone()
//        {
//            throw new NotImplementedException();
//        }}}

