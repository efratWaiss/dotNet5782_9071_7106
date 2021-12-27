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
    public class BL : IBL
    {
        IDal.IDal dal = new DalObject.DalObject();
        public List<DroneToList> DronesList = new();

        private double available;
        private double lightWeight;
        public double MediumWeight { get; }
        private double heavyWeight;

        public BL()
        {
            dal = new DalObject.DalObject();
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
                        //electricity = (int) ((charge* getDistance(distance, getCustomerLocation(p.senderId))) +
                        //                (getDistance(getCustomerLocation(p.senderId), minLocation(location)) * avaleble));
                        battery = 45.3;///לסדר את הבטרייה 
                        DroneToList d = new DroneToList(drone.Id, drone.Model, (WeightCategories)drone.MaxWeight, battery, st, lo, parcel.Id);
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
                        d = new Drone(drone.Id, drone.Model, (WeightCategories)drone.MaxWeight, DroneStatuses.Vacant, battery/*לבדוק*/, null, lo);
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
         
        public void addCustomer(int Id, String Name, String Phone, double Longitude, double Latitude)
        {
            try
            {
                dal.addCustomer(new IDAL.DO.Customer(Id, Name, Phone, Longitude, Latitude));
            }
            catch
            {
                throw new NotImplementedException();
            }

        }//לבדוק לגבי excption

        public void addDrone(int Id, String Model, WeightCategories weight, int IdStation)// exption בעיה
        {
            try
            {
                var drone = dal.printDrone(Id);
                Random rn = new Random();
                double battery = rn.Next(20, 41);
                try
                {
                    var Station1 = dal.printStation(IdStation);//לא נכון
                    dal.addDrone(new IDAL.DO.Drone(Id, Model, (IDAL.DO.WeightCategories)(weight)));
                    DronesList.Add(new DroneToList(Id, Model, weight, battery, DroneStatuses.Maintenance, new Location(Station1.Latitude, Station1.Longitude), IdStation));
                }
                catch 
                { 
                    throw new NotImplementedException();
                }
            }
            catch
            {
                throw new NotImplementedException();
            }

        }


        public int addParcel(int SenderId, int TargetId, WeightCategories Weight, Priorities Priorities)// exption בעיה
        {
            try
            {
                var customer1 = dal.printCustomer(SenderId);
                var customer2 = dal.printCustomer(TargetId);
                dal.addParcel(new IDAL.DO.Parcel(SenderId, TargetId, (IDAL.DO.WeightCategories)Weight, (IDAL.DO.Priorities)Priorities, DateTime.Now, 0));
            }
            catch
            {
                throw new NotImplementedException();
            }

            return Parcel.Id;
        }

        public void addStation(int Id, String Name, Location location, int AvailableChargeSlots)//exption  וגם הוספה blבעיה
        {
            try
            {
                var station = dal.printStation(Id);
                //StationsList.Add(new Station(Id,Name,Location,ChargingPositions, new()));///איך להכניס ךbl
                dal.addStation(new IDAL.DO.Station(Id, Name, location.Latitude, location.Longitude, AvailableChargeSlots));
            }

            catch
            { // :אם מצאת כבר רחפן כזה, זרוק 
                throw new NotImplementedException();
            }

        }

        public void freeDrone(int idDrone, double timeInCharging)//בעיהה
        {

            var drone = DronesList.FirstOrDefault(x => x.Id == idDrone);
            if (drone.Id != 0)
            {
                if (drone.Status == DroneStatuses.Maintenance)
                {
                    drone.Battery += timeInCharging * ChargingRate;
                    drone.Status = DroneStatuses.Vacant;
                    var station = dal.printStation(drone.ParcelDelivered);
                    station.ChargeSlots += 1;
                    var k = dal.printDroneCharge(drone.Id);
                    dal.GetDroneCharges().Remove(k);//איך למחוק מרשימה?

                }
                else
                {
                    throw new NotImplementedException("the drone's status is not Maintenance");
                }
            }
            else
            {
                throw new NotImplementedException("this id drone not exist in the system");
            }

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
            try
            {
                var customer = dal.printCustomer(id);
                Customer c = new Customer(customer.Id, customer.Name, customer.Phone, new Location(customer.Longitude, customer.Latitude));
                foreach (var item in dal.viewParcel())
                {
                    if (item.SenderId == c.Id)
                        c.parcelToCustomer.Add(new Parcel(c, dal.printCustomer(item.TargetId), (WeightCategories)item.Weight, (Priorities)item.priority));//להוסיף תאריכים?
                    else if (item.TargetId == c.Id)
                        c.parcelFromCustomer.Add(new Parcel(dal.printCustomer(item.SenderId), c, (WeightCategories)item.Weight, (Priorities)item.priority));
                }
                return c;
            }
            catch
            {
                throw new NotImplementedException();
            }

        }//בעיה בתאריכים איך למלא?

        public Drone printDrone(int id)
        {
            //Drone drone1=new();
            ParcelInTransference ParcelInTransference;
            var drone = DronesList.FirstOrDefault(x => x.Id == id);
            var parcel = DronesList.FirstOrDefault(x => x.Id == drone.ParcelDelivered);
            if (drone.Id != 0)
            {
                if (drone.Status == DroneStatuses.Shipping)
                {
                    var p = dal.printParcel(drone.ParcelDelivered);
                    ParcelInTransference = new(p.Id, new CustomerInParcel(p.SenderId, dal.printCustomer(p.SenderId).Name), new CustomerInParcel(p.TargetId, dal.printCustomer(p.TargetId).Name), (WeightCategories)p.Weight, (Priorities)p.priority, true,
                       new Location(dal.printCustomer(p.SenderId).Longitude, dal.printCustomer(p.SenderId).Latitude),
                       new Location(dal.printCustomer(p.TargetId).Longitude, dal.printCustomer(p.TargetId).Latitude)
                       , GetDistanceBetweenTwoLocation(new Location(dal.printCustomer(p.SenderId).Longitude, dal.printCustomer(p.SenderId).Latitude),
                       new Location(dal.printCustomer(p.TargetId).Longitude, dal.printCustomer(p.TargetId).Latitude)));
                }
                else
                {
                    ParcelInTransference = null;
                }
                Drone d = new Drone(drone.Id, drone.Model, drone.MaxWeight, drone.Status, drone.Battery, ParcelInTransference, drone.LocationNow);
                return d;
            }
            else
            {
                throw new NotImplementedException("this id not exist in the system");
            }

        }

        public Parcel printParcel(int id)
        {
            Parcel p;
            try
            {
                var parcel = dal.printParcel(id);
                if (parcel.DroneId != 0)
                {
                    var drone = DronesList.FirstOrDefault(x => x.Id == parcel.DroneId);
                    p = new(dal.printCustomer(parcel.SenderId),dal.printCustomer(parcel.TargetId),(WeightCategories)parcel.Weight,(Priorities)parcel.priority
                        ,,,parcel.PickedUp,new DroneInParcel(drone.Id,drone.Battery,drone.LocationNow));
                }
                else
                {
                    p = new(dal.printCustomer(parcel.SenderId), dal.printCustomer(parcel.TargetId), (WeightCategories)parcel.Weight, (Priorities)parcel.priority
                        ,,, parcel.PickedUp,null) ;
                }
                return p;
            }
            catch
            {
                throw new NotImplementedException();
            }


        }//בעיה בתאריכים איך למלא?

        public Station printStation(int id)
        {
            List<DroneInCharging> d = new();
            try
            {
                var stationDal = dal.printStation(id);

                foreach (var item in dal.GetDroneCharges())
                {
                    if (item.Stationld == stationDal.Id)
                    {

                        d.Add(new DroneInCharging(item.Droneld, (DronesList.FirstOrDefault(x => x.Id == item.Droneld)).Battery));
                    }
                }
                return new Station(stationDal.Id, stationDal.Name, new Location(stationDal.Longitude, stationDal.Latitude), stationDal.ChargeSlots, d); ;
            }
            catch
            {
                throw new NotImplementedException("this id not exist in the system");

            }

        }

        public void sendDroneToStation(int idDrone)//בעיה
        {
            double min = double.MaxValue;
            double distance = 0;
            var stationHelp = dal.viewStation().FirstOrDefault(x => x.Id == 1);//אתחול
            var drone = DronesList.FirstOrDefault(x => x.Id == idDrone);
            if (drone.Id != 0)
            {
                if (drone.Status == DroneStatuses.Vacant)
                {
                    foreach (var station in dal.viewStation())
                    {
                        distance = GetDistanceBetweenTwoLocation(new Location(station.Longitude, station.Latitude), drone.LocationNow);
                        if (distance < min && station.ChargeSlots != 0/*לבדוק*/&& drone.Battery >= distance * available)
                        {
                            stationHelp = station;
                        }
                    }
                    drone.Battery = drone.Battery - distance;
                    drone.LocationNow = new Location(stationHelp.Longitude, stationHelp.Latitude);
                    drone.Status = DroneStatuses.Maintenance;
                    stationHelp.ChargeSlots -= 1;//לבדוק את התחנות הפנויות
                    dal.GetDroneCharges().ToList().Add(new IDAL.DO.DroneCharge(drone.Id, stationHelp.Id));
                }
                else
                {
                    throw new NotImplementedException("this drone's status not vacant ");
                }
            }
            else
            {
                throw new NotImplementedException("this Drone's id not exist in the system");
            }
        }//בעיה

        public IEnumerable<Station> StationNoCharge()
        {
            throw new NotImplementedException();
        }

        public void targetId(int idCustomer, int idParcel)
        {
            throw new NotImplementedException();
        }

        public void updateParcelToDrone(int idDrone)
        {
            try
            {
                var drone = dal.printDrone(idDrone);
                var drone1 = DronesList.FirstOrDefault(x => x.Id == idDrone);
                if (drone1.Status == DroneStatuses.Vacant)
                {

                }
                else
                {
                    throw new NotImplementedException("the drone is not vacant");
                }
            }
            catch
            {
                throw new NotImplementedException();
            }


        }

        public IEnumerable<customerToList> viewCustomer()//בעיה
        {
            var customers = dal.viewCustomer();
            List<customerToList> c = new List<customerToList>();
            foreach (var item in customers)
            {
                c.Add(new customerToList(item.Id, item.Phone, item.))//צריך להביא
            }
            return c;
        }

        public IEnumerable<DroneToList> viewDrone()
        {
            return new List<DroneToList>(DronesList);
        }

        public IEnumerable<ParcelToList> viewParcel()
        {
            var parcels = dal.viewParcel();
            List<ParcelToList> p = new List<ParcelToList>();
            foreach (var item in parcels)
            {
                p.Add(new ParcelToList(item.Id, item.SenderId, item.TargetId, item.Weight, item.priority, item.));
            }
            return p;
        }//בעיה

        public IEnumerable<StationToList> viewStation()//בעיה
        {
            var stations = dal.viewStation();
            List<StationToList> s = new List<StationToList>();
            foreach (var item in stations)
            {
                s.Add(new StationToList(item.Id, item.Name, item.ChargeSlots, item.ChargeSlots));///לא טוב איך יודעים על עמדות הטעינה?
            }
            return s;
        }
        public void UpdateNameDrone(int Id, String Model)
        {
            try
            {
                var Drone = dal.printDrone(Id);
                Drone.Model = Model;
                var Drone1 = DronesList.FirstOrDefault(x => x.Id == Id);
                Drone1.Model = Model;
            }
            catch
            {
                throw new NotImplementedException();
            }

        }
        public void UpdateStationDetails(int IdStation, String? NameStation, int? ChargeSlots)
        {
            try
            {
                var Station = dal.printStation(IdStation);
                if (!NameStation.Equals(default))
                {
                    Station.Name = NameStation;
                }
                if (!ChargeSlots.Equals(default))
                {
                    Station.ChargeSlots = (int)ChargeSlots;
                }

            }
            catch
            {
                throw new NotImplementedException();
            }

        }
        public void UpdateCustomerDetails(int IdCustomer, String? Name, String? Phone)
        {
            try
            {
                var customer = dal.printCustomer(IdCustomer);
                if (!Name.Equals(default))
                {
                    customer.Name = Name;
                }
                if (!Phone.Equals(default))
                {
                    customer.Phone = Phone;
                }

            }
            catch
            {
                throw new NotImplementedException();
            }
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

