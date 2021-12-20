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
        private readonly List<Drone> DronesList;
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

            var parcels = dal.viewParcel();
            var Drones = dal.viewDrone();
            var Customers = dal.viewCustomer();
            var Station = dal.viewStation().ToList();
            DroneStatuses st = new DroneStatuses();
            double battery = new double();
            Location lo = new Location(454.5, 45.5);

            foreach (var drone in Drones)
            {
                foreach (var parcel in parcels)
                {
                    if (parcel.DroneId == drone.Id && parcel.Delivered != null)
                    {
                       // ParcelInTransference p = new ParcelInTransference(parcel.Id, parcel.SenderId, parcel.TargetId, parcel.Weight, parcel.priority, true, null, null, null);

                        var customer = dal.viewCustomer().FirstOrDefault(x => x.Id == parcel.SenderId);
                        st = DroneStatuses.Shipping;
                        if (parcel.scheduled != null && parcel.PickedUp == null)
                        {
                            
                            double min = double.MaxValue;
                            foreach (var station in Station)
                            {
                                if (GetDistanceBetweenTwoLocation(new Location(station.Latitude, station.Longitude),new Location(customer.Latitude,customer.Longitude)) < min)
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
                        int rr = rn.Next(1, Station.Count());
                        lo.Latitude = Station[rr].Latitude;
                        lo.Longitude = Station[rr].Longitude;
                        Drone d = new Drone(drone.Id, drone.Model, (WeightCategories)drone.MaxWeight, DroneStatuses.Maintenance, rn.Next(0, 21), null, lo);
                        break;
                    case 2:
                        foreach (var Customer in Customers)
                        {
                            foreach (var Parcel in parcels)
                            {
                                if (Customer.Id == Parcel.TargetId)
                                {
                                    IDs.Add(Customer.Id);//מכניס את הת''ז של הלקוחות שסופקו להם חבילות
                                }
                            }
                            
                        }
                        rand = rn.Next(1, IDs.Count());
                        foreach (var Customer in Customers)
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

