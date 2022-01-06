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
        IDal.IDal dal;
        public List<DroneToList> DronesList = new();

        private double available;//רחפן ריק
        private double lightWeight;//קל
        public double MediumWeight { get; }//מדיום
        private double heavyWeight;//כבד
        private double ChargingRate;

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
        public void AddCustomer(int Id, String Name, String Phone, double Longitude, double Latitude)
        {
            try
            {
                dal.AddCustomer(new IDAL.DO.Customer(Id, Name, Phone, Longitude, Latitude));
            }
            catch (IdException ex)
            {
                throw (ex);
            }

        }
        public void AddDrone(int Id, String Model, WeightCategories weight, int IdStation)
        {
            try
            {
                var drone = dal.GetDrone(Id);
                Random rn = new Random();
                double battery = rn.Next(20, 41);
                try
                {
                    var Station1 = dal.GetStation(IdStation);
                    dal.AddDrone(new IDAL.DO.Drone(Id, Model, (IDAL.DO.WeightCategories)(weight)));
                    DronesList.Add(new DroneToList(Id, Model, weight, battery, DroneStatuses.Maintenance, new Location(Station1.Latitude, Station1.Longitude), -1));
                }
                catch (IdException e)
                {
                    throw e;
                }
            }
            catch (IdException e)
            {
                throw (e);
            }

        }
        public int AddParcel(int SenderId, int TargetId, WeightCategories Weight, Priorities Priorities)
        {
            try
            {
                var customer1 = dal.GetCustomer(SenderId);
                var customer2 = dal.GetCustomer(TargetId);
                dal.AddParcel(new IDAL.DO.Parcel(SenderId, TargetId, (IDAL.DO.WeightCategories)Weight, (IDAL.DO.Priorities)Priorities, DateTime.Now, 0));
            }
            catch (IdException e)
            {
                throw (e);
            }

            return Parcel.Id;
        }
        public void AddStation(int Id, String Name, Location location, int AvailableChargeSlots)
        {
            try
            {
                var station = dal.GetStation(Id);
                dal.AddStation(new IDAL.DO.Station(Id, Name, location.Latitude, location.Longitude, AvailableChargeSlots));
            }

            catch (IdException e)
            { // :אם מצאת כבר רחפן כזה, זרוק 
                throw (e);
            }

        }
        public void FreeDrone(int idDrone, double timeInCharging)
        {
            IDAL.DO.Station station;
            var drone = DronesList.FirstOrDefault(x => x.Id == idDrone);
            if (drone != default)
            {
                if (drone.Status == DroneStatuses.Maintenance)
                {
                    drone.Battery += timeInCharging * ChargingRate * droneWeight(drone.Id);//TODO: האם זה כך?
                    drone.Status = DroneStatuses.Vacant;
                    try
                    {
                        station = dal.GetStation(drone.ParcelDelivered);
                        station.ChargeSlots += 1;
                    }
                    catch
                    {
                        throw new NotImplementedException("the drone's parcel did not Delivered");
                    }
                    try
                    {
                        dal.removeFromDroneCharges(idDrone, station.Id);
                    }
                    catch (IdException e)
                    {
                        throw (e);
                    }

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
        public Customer GetCustomer(int id)
        {
            try
            {
                var customer = dal.GetCustomer(id);
                Customer c = new Customer(customer.Id, customer.Name, customer.Phone, new Location(customer.Longitude, customer.Latitude));
                foreach (var item in dal.GetListParcel())
                {
                    if (item.SenderId == c.Id)
                        c.parcelToCustomer.Add(new Parcel(new CustomerInParcel(c.Id, c.Name)
                            , new CustomerInParcel(dal.GetCustomer(item.TargetId).Id, dal.GetCustomer(item.TargetId).Name)
                            , (WeightCategories)item.Weight
                            , (Priorities)item.priority
                            , new DroneInParcel(DronesList.FirstOrDefault(x => x.Id == item.DroneId).Id, DronesList.FirstOrDefault(x => x.Id == item.DroneId).Battery, DronesList.FirstOrDefault(x => x.Id == item.DroneId).LocationNow)
                            , item.Requested
                            , DronesList.FirstOrDefault(x => x.Id == item.DroneId).Id
                            , item.scheduled
                            , item.PickedUp.Value
                            , item.Delivered.Value));
                    else if (item.TargetId == c.Id)
                        c.parcelFromCustomer.Add(new Parcel(new CustomerInParcel(dal.GetCustomer(item.SenderId).Id, dal.GetCustomer(item.SenderId).Name)
                           , new CustomerInParcel(c.Id, c.Name)
                           , (WeightCategories)item.Weight
                           , (Priorities)item.priority
                           , new DroneInParcel(DronesList.FirstOrDefault(x => x.Id == item.DroneId).Id, DronesList.FirstOrDefault(x => x.Id == item.DroneId).Battery, DronesList.FirstOrDefault(x => x.Id == item.DroneId).LocationNow)
                           , item.Requested
                           , DronesList.FirstOrDefault(x => x.Id == item.DroneId).Id
                           , item.scheduled.Value
                           , item.PickedUp.Value
                           , item.Delivered.Value));
                }
                return c;
            }
            catch (IdException e)
            {
                throw (e);
            }

        }
        public Drone GetDrone(int id)
        {

            ParcelInTransference ParcelInTransference;
            var drone = DronesList.FirstOrDefault(x => x.Id == id);
            var parcel = DronesList.FirstOrDefault(x => x.Id == drone.ParcelDelivered);
            if (drone != default)
            {
                if (drone.Status == DroneStatuses.Shipping)
                {
                    try
                    {
                        var p = dal.GetParcel(drone.ParcelDelivered);
                        ParcelInTransference = new(p.Id, new CustomerInParcel(p.SenderId, dal.GetCustomer(p.SenderId).Name), new CustomerInParcel(p.TargetId, dal.GetCustomer(p.TargetId).Name), (WeightCategories)p.Weight, (Priorities)p.priority, true,
                           new Location(dal.GetCustomer(p.SenderId).Longitude, dal.GetCustomer(p.SenderId).Latitude),
                           new Location(dal.GetCustomer(p.TargetId).Longitude, dal.GetCustomer(p.TargetId).Latitude)
                           , GetDistanceBetweenTwoLocation(new Location(dal.GetCustomer(p.SenderId).Longitude, dal.GetCustomer(p.SenderId).Latitude),
                           new Location(dal.GetCustomer(p.TargetId).Longitude, dal.GetCustomer(p.TargetId).Latitude)));
                    }
                    catch (IdException e)
                    {
                        throw e;
                    }
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
        public Parcel GetParcel(int id)
        {
            Parcel p = default;
            try
            {
                var parcel = dal.GetParcel(id);
                if (parcel.DroneId != 0)
                {
                    var drone = DronesList.FirstOrDefault(x => x.Id == parcel.DroneId);
                    p = new Parcel(new CustomerInParcel(dal.GetCustomer(parcel.SenderId).Id, dal.GetCustomer(parcel.SenderId).Name), new CustomerInParcel(dal.GetCustomer(parcel.TargetId).Id, dal.GetCustomer(parcel.TargetId).Name), (WeightCategories)parcel.Weight, (Priorities)parcel.priority
                     , new DroneInParcel(drone.Id, drone.Battery, drone.LocationNow), parcel.Requested, parcel.DroneId, parcel.scheduled
                    , parcel.PickedUp, parcel.Delivered);
                }
                else
                {
                    p = new Parcel(new CustomerInParcel(dal.GetCustomer(parcel.SenderId).Id, dal.GetCustomer(parcel.SenderId).Name), new CustomerInParcel(dal.GetCustomer(parcel.TargetId).Id, dal.GetCustomer(parcel.TargetId).Name), (WeightCategories)parcel.Weight, (Priorities)parcel.priority
                     , null, parcel.Requested, parcel.DroneId, parcel.scheduled, parcel.PickedUp, parcel.Delivered);
                }
                return p;
            }
            catch
            {
                throw new NotImplementedException();
            }

        }
        public Station GetStation(int id)
        {
            List<DroneInCharging> d = new();
            try
            {
                var stationDal = dal.GetStation(id);

                foreach (var item in dal.GetListDroneCharges())
                {
                    if (item.Stationld == stationDal.Id)
                    {

                        d.Add(new DroneInCharging(item.Droneld, (DronesList.FirstOrDefault(x => x.Id == item.Droneld)).Battery));
                    }
                }
                return new Station(stationDal.Id, stationDal.Name, new Location(stationDal.Longitude, stationDal.Latitude), stationDal.ChargeSlots, d); ;
            }
            catch (NotExistException e)
            {
                throw (e);

            }

        }
        public void SendDroneToStation(int idDrone)
        {
            double min = double.MaxValue;
            double distance = 0;
            var stationHelp = dal.GetListStation().FirstOrDefault(x => x.Id == 1);//אתחול
            var drone = DronesList.FirstOrDefault(x => x.Id == idDrone);
            if (drone != default)
            {
                double wightDrone = droneWeight(drone.Id);
                if (drone.Status == DroneStatuses.Vacant)
                {
                    foreach (var station in dal.GetListStation())
                    {
                        distance = GetDistanceBetweenTwoLocation(new Location(station.Longitude, station.Latitude), drone.LocationNow);
                        if (distance < min && station.ChargeSlots != 0/*לבדוק*/&& drone.Battery > distance * wightDrone * ChargingRate)
                        {
                            stationHelp = station;
                        }
                    }
                    drone.Battery = drone.Battery - (distance * wightDrone * ChargingRate);
                    drone.LocationNow = new Location(stationHelp.Longitude, stationHelp.Latitude);
                    drone.Status = DroneStatuses.Maintenance;
                    stationHelp.ChargeSlots -= 1;//TODO:לבדוק את התחנות הפנויות
                    dal.GetListDroneCharges().ToList().Add(new IDAL.DO.DroneCharge(drone.Id, stationHelp.Id));
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
        }
        public void UpdateParcelToDrone(int idDrone)
        {
            try
            {
                double Latitude = 0;
                double Longitude = 0;
                int index = 0;
                int start = 0;
                IDAL.DO.Parcel temp;
                IDAL.DO.Parcel parcelChoise = default;
                int end = dal.GetListParcel().Count();
                double minLocation = 99999;
                double tempLocation = 0;
                var p = dal.GetListParcel().ToList();
                List<Parcel> parcels = new List<Parcel>();
                List<Parcel> tempParcels = new List<Parcel>();

                var drone1 = DronesList.FirstOrDefault(x => x.Id == idDrone);
                if (drone1 != default)
                {
                    if (drone1.Status == DroneStatuses.Vacant)//האם הרחפן פנוי 
                    {

                        for (int i = 0; i < dal.GetListParcel().Count(); i++)
                        {//ממיין את הרשימה מעדיפות גבוהה לנמוכה
                            double min = double.MaxValue;
                            foreach (var station in dal.GetListStation())//תחנה קרובה למקבל
                            {
                                if (GetDistanceBetweenTwoLocation(new Location(station.Latitude, station.Longitude), drone1.LocationNow) < min)
                                {
                                    min = GetDistanceBetweenTwoLocation(new Location(station.Latitude, station.Longitude), drone1.LocationNow);
                                    Latitude = station.Latitude;
                                    Longitude = station.Longitude;
                                }
                            }

                            tempLocation =
                                GetDistanceBetweenTwoLocation(new Location(dal.GetCustomer(p[i].SenderId).Longitude, dal.GetCustomer(p[i].SenderId).Latitude), drone1.LocationNow)
                                + GetDistanceBetweenTwoLocation(new Location(dal.GetCustomer(p[i].TargetId).Longitude, dal.GetCustomer(p[i].TargetId).Latitude), drone1.LocationNow)
                              + GetDistanceBetweenTwoLocation(new Location(dal.GetCustomer(p[i].TargetId).Longitude, dal.GetCustomer(p[i].TargetId).Latitude), new Location(Longitude, Latitude));

                            //tempLocation שומר את כל הדרך שעל הרחפן לעבור, כדי להעביר חבילה מלקוח למקבל ולהגיע לתחנה הקרובה לטעינה (כדי לבדוק אם יש לו מספיק בטריה
                            if (tempLocation <= drone1.Battery * ChargingRate * droneWeight(drone1.Id))//בודק האם קיימת מספיק בטריה להמשך הדרך
                            {
                                if ((drone1.MaxWeight == WeightCategories.Intermediate && (WeightCategories)p[i].Weight != WeightCategories.Liver) || (drone1.MaxWeight == WeightCategories.Easy && (WeightCategories)p[i].Weight == WeightCategories.Easy))
                                {
                                    if ((Priorities)p[i].priority == Priorities.Regular)
                                    {
                                        temp = p[end];
                                        p[end] = p[i];
                                        p[i] = temp;
                                        end--;
                                    }
                                    else if ((Priorities)p[i].priority == Priorities.Emergency)
                                    {
                                        temp = p[start];
                                        p[start] = p[i];
                                        p[i] = temp;
                                        start++;
                                    }
                                }
                                else
                                {
                                    p.Remove(p[i]);//מוחק מהרשימה
                                }
                            }
                        }
                        while (p[0].priority == p[index].priority)
                        {
                            index++;
                        }
                        start = 0;
                        end = index;
                        for (int i = 0; i < index; i++)
                        {//ממיין לפי משקל חבילה מרבי
                            if ((WeightCategories)p[i].Weight == WeightCategories.Easy)
                            {
                                temp = p[end];
                                p[end] = p[i];
                                p[i] = temp;
                                end--;
                            }
                            else if ((WeightCategories)p[i].Weight == WeightCategories.Liver)
                            {
                                temp = p[start];
                                p[start] = p[i];
                                p[i] = temp;
                                start++;
                            }
                        }
                        index = 0;
                        while (p[0].Weight == p[index].Weight)
                        {
                            index++;
                        }
                        for (int i = 0; i < index; i++)
                        {
                            tempLocation = GetDistanceBetweenTwoLocation(
                                new Location(dal.GetCustomer(p[i].SenderId).Longitude, dal.GetCustomer(p[i].SenderId).Latitude)
                                , drone1.LocationNow);
                            if (minLocation > tempLocation)
                            {
                                minLocation = tempLocation;
                                parcelChoise = p[i];
                            }
                        }
                        if (parcelChoise.Equals(default))
                        {
                            drone1.Status = DroneStatuses.Shipping;
                            parcelChoise.DroneId = drone1.Id;
                            parcelChoise.Delivered = DateTime.Now;
                        }
                    }
                }

                else
                {
                    throw new NotImplementedException("this Drone's id not exist in the system");
                }

            }
            catch (IdException e)
            {
                throw (e);
            }
        }
        public IEnumerable<ParcelToList> ParcelNoDrone()
        {
            try
            {
                var parcels = GetListParcel();
                bool provided = false;
                List<ParcelToList> newParcelNoDrone = new List<ParcelToList>();
                foreach (var item in parcels)
                {
                    foreach (var item1 in DronesList)
                    {
                        if (item1.ParcelDelivered == item.Id)
                        {
                            provided = true;
                        }
                    }
                    if (provided == false)
                    {
                        newParcelNoDrone.Add(item);
                    }
                }
                return newParcelNoDrone;
            }
            catch (IdException e)
            {
                throw e;
            }
        }
        public void PackageCollectionByDrone(int idDrone)
        {
            try
            {
                var pacelsL = dal.GetListParcel().ToList();
                var drone = dal.GetDrone(idDrone);
                DroneToList GetListDrone = DronesList.FirstOrDefault(x => x.Id == idDrone);
                var itemParcel = dal.GetParcel(GetListDrone.ParcelDelivered);
                for (int i = 0; i < dal.GetListParcel().Count(); i++)
                {

                    if (pacelsL[i].DroneId == idDrone && pacelsL[i].PickedUp.Equals(default))
                    {
                        GetListDrone.Battery = GetListDrone.Battery - (GetDistanceBetweenTwoLocation(GetListDrone.LocationNow,
                             new Location(dal.GetCustomer(pacelsL[i].SenderId).Longitude, dal.GetCustomer(pacelsL[i].TargetId).Latitude))) * ChargingRate * droneWeight(pacelsL[i].DroneId);
                        GetListDrone.LocationNow = new Location(dal.GetCustomer(pacelsL[i].SenderId).Longitude, dal.GetCustomer(pacelsL[i].TargetId).Latitude);
                        itemParcel.PickedUp = DateTime.Now;
                        dal.UpdateParcel(itemParcel);
                    }
                    else
                    {

                        throw new NotImplementedException("The package was not associated with this skimmer or the package has already been collected");
                    }
                }
            }
            catch (IdException e)
            {
                throw (e);
            }
        }
        public void DeliveryOfAParcelByDrone(int idDrone)
        {
            try
            {

                var parcels = dal.GetListParcel().ToList();
                var drone = dal.GetDrone(idDrone);
                DroneToList GetListDrone = DronesList.FirstOrDefault(x => x.Id == idDrone);
                var itemParcel = dal.GetParcel(GetListDrone.ParcelDelivered);
                for (int i = 0; i < dal.GetListParcel().Count(); i++)
                {
                    if (parcels[i].DroneId == idDrone && !parcels[i].PickedUp.Equals(default) && parcels[i].Delivered.Equals(default))
                    {
                        GetListDrone.Battery = GetListDrone.Battery - (GetDistanceBetweenTwoLocation(GetListDrone.LocationNow,
                             new Location(dal.GetCustomer(parcels[i].TargetId).Longitude, dal.GetCustomer(parcels[i].TargetId).Latitude))) * ChargingRate;
                        GetListDrone.LocationNow = new Location(dal.GetCustomer(parcels[i].SenderId).Longitude, dal.GetCustomer(parcels[i].SenderId).Latitude);
                        GetListDrone.Status = DroneStatuses.Vacant;
                        itemParcel.Delivered = DateTime.Now;
                        dal.UpdateParcel(itemParcel);
                    }
                    else
                    {
                        throw new NotImplementedException("The package has not been collected or the package has already been delivered or the skimmer is not associated with any package");
                    }

                }
            }
            catch (IdException e)
            {
                throw (e);
            }
        }
        public IEnumerable<StationToList> GetListStation()
        {
            var stations = dal.GetListStation();
            List<StationToList> s = new List<StationToList>();
            foreach (var item in stations)
            {
                int count = 0;
                foreach (var item1 in dal.GetListDroneCharges())
                {
                    if (item1.Stationld == item.Id)
                    {
                        count++;
                    }
                }
                s.Add(new StationToList(item.Id, item.Name, item.ChargeSlots - count, count));
            }
            return s;
        }
        public IEnumerable<DroneToList> GetListDrone()
        {
            return new List<DroneToList>(DronesList);
        }
        public IEnumerable<CustomerToList> GetListCustomer()
        {
            var customers = dal.GetListCustomer();
            List<CustomerToList> c = new List<CustomerToList>();
            int countParcelProvided = 0;//חבילות שנשלחו וסופקו
            int countParcelNotProvided = 0;//מונה החבילות שנשלחו ולא סופקו
            int countGetListParcels = 0;//חבילות שקיבלתי
            foreach (var item in customers)
            {
                foreach (var item1 in dal.GetListParcel())
                {
                    if (item1.SenderId == item.Id)//אם החבילה נשלחה, תבדוק הלאה
                    {
                        if (!item1.Delivered.Equals(default))//אם החבילה גם סופקה,
                        //תקדם את מונה החבילות שסופקו ונשלחו
                        {
                            countParcelProvided++;
                        }
                        else
                        {//אחרת, קדם את מונה החבילות שנשלחו ולא סופקו
                            countParcelNotProvided++;
                        }
                    }
                    else if (item1.TargetId == item.Id)
                    {//אם קיבלתי חבילה
                        countGetListParcels++;
                    }

                }
                c.Add(new CustomerToList(item.Id, item.Name, item.Phone, countParcelProvided, countParcelNotProvided, countGetListParcels, countParcelNotProvided + countParcelProvided));//צריך להביא
            }
            return c;
        }
        public IEnumerable<ParcelToList> GetListParcel()
        {
            try
            {
                var parcels = dal.GetListParcel();
                ParcelStatsus temp;
                List<ParcelToList> p = new List<ParcelToList>();
                foreach (var item in parcels)
                {
                    if (!item.PickedUp.Equals(default))//נאסף=נאסף
                    {
                        temp = ParcelStatsus.collected;
                    }
                    else if (!item.Delivered.Equals(default))//נמסר=קשור ל...
                    {
                        temp = ParcelStatsus.associated;
                    }
                    else if (!item.Requested.Equals(default))//מבוקש=נוצר
                    {
                        temp = ParcelStatsus.created;
                    }
                    else if (!item.scheduled.Equals(default))//נמסר=קשור ל
                    {
                        temp = ParcelStatsus.Defined;
                    }
                    else
                    {
                        temp = ParcelStatsus.provided;
                    }


                    p.Add(new ParcelToList(item.Id, new CustomerInParcel(dal.GetCustomer(item.SenderId).Id, dal.GetCustomer(item.SenderId).Name)
                       , new CustomerInParcel(dal.GetCustomer(item.TargetId).Id, dal.GetCustomer(item.TargetId).Name)
                         , (WeightCategories)item.Weight
                         , (Priorities)item.priority
                         , temp));
                }
                return p;
            }
            catch (NotExistException e)
            {
                throw ;
            }
        }//TODO: לא עובד
        public IEnumerable<StationToList> StationWithAvailableStands()
        {
            List<StationToList> stands = new List<StationToList>();
            var stations = GetListStation();
            foreach (var item in stations)
            {
                if (item.AvailableChargingPositions != 0)
                {
                    stands.Add(item);
                }
            }
            return stands;
        }
        public void UpdateNameDrone(int Id, String Model)
        {
            try
            {
                var Drone = dal.GetDrone(Id);
                Drone.Model = Model;
                var Drone1 = DronesList.FirstOrDefault(x => x.Id == Id);
                Drone1.Model = Model;
            }
            catch (IdException e)
            {
                throw (e);
            }

        }
        public void UpdateStationDetails(int IdStation, String? NameStation, int? ChargeSlots)
        {
            try
            {
                var Station = dal.GetStation(IdStation);
                if (!NameStation.Equals(default))
                {
                    Station.Name = NameStation;
                }
                if (!ChargeSlots.Equals(default))
                {
                    Station.ChargeSlots = (int)ChargeSlots;
                }

            }
            catch (IdException e)
            {
                throw (e);
            }

        }
        public void UpdateCustomerDetails(int IdCustomer, String? Name, String? Phone)
        {
            try
            {
                var customer = dal.GetCustomer(IdCustomer);
                if (!Name.Equals(default))
                {
                    customer.Name = Name;
                }
                if (!Phone.Equals(default))
                {
                    customer.Phone = Phone;
                }

            }
            catch (IdException e)
            {
                throw (e);
            }
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


