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
                        
                       double tempLocation =
                               GetDistanceBetweenTwoLocation(new Location(dal.printCustomer(parcel.SenderId).Longitude, dal.printCustomer(parcel.SenderId).Latitude), DronesList.FirstOrDefault(x=>x.Id==drone.Id).LocationNow)//מרחק בין מיקום רחפן לשולח
                               + GetDistanceBetweenTwoLocation(new Location(dal.printCustomer(parcel.SenderId).Longitude, dal.printCustomer(parcel.SenderId).Latitude),new Location(dal.printCustomer(parcel.TargetId).Longitude, dal.printCustomer(parcel.TargetId).Latitude))//מרחק בין שולח למקבל
                             + GetDistanceBetweenTwoLocation(new Location(dal.printCustomer(parcel.TargetId).Longitude, dal.printCustomer(parcel.TargetId).Latitude), new Location(lo.Longitude,lo.Latitude));
                        Random rn1 = new Random();
                        battery = rn1.Next((int)(tempLocation * ChargingRate * available),100);
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
                        //TODO: !!!!!!!!
                        d = new Drone(drone.Id, drone.Model, (WeightCategories)drone.MaxWeight, DroneStatuses.Vacant, battery, null, lo);
                        break;
                }



            }

            throw new NotImplementedException();
        }//TODO: חסרה לנו הבטריה
        public double GetDistanceBetweenTwoLocation(Location l1, Location l2)
        {
            return Math.Sqrt(Math.Pow(l1.Latitude - l2.Latitude, 2) + Math.Pow(l1.Longitude - l2.Longitude, 2));
        }
        public double CarryingHeavyWeight { get; }
        public void addCustomer(int Id, String Name, String Phone, double Longitude, double Latitude)
        {
            try
            {
                dal.addCustomer(new IDAL.DO.Customer(Id, Name, Phone, Longitude, Latitude));
            }
            catch (IdException ex)
            {
                throw (ex);
            }

        }
        public void addDrone(int Id, String Model, WeightCategories weight, int IdStation)//TODO:וגם ההחזרה לבדוק לגבי 
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
                catch (IdException e)
                {
                    throw (e);
                }
            }
            catch (IdException e)
            {
                throw (e);
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
            catch (IdException e)
            {
                throw (e);
            }

            return Parcel.Id;
        }
        public void addStation(int Id, String Name, Location location, int AvailableChargeSlots)//TODO: לבדוק לגבי
        {
            try
            {
                var station = dal.printStation(Id);
                List<IDAL.DO.DroneCharge> d = new();

                dal.addStation(new IDAL.DO.Station(Id, Name, location.Latitude, location.Longitude, AvailableChargeSlots));
            }

            catch (IdException e)
            { // :אם מצאת כבר רחפן כזה, זרוק 
                throw (e);
            }

        }
        public void freeDrone(int idDrone, double timeInCharging)
        {

            var drone = DronesList.FirstOrDefault(x => x.Id == idDrone);
            if (drone != default)
            {
                if (drone.Status == DroneStatuses.Maintenance)
                {
                    drone.Battery += timeInCharging * ChargingRate;
                    drone.Status = DroneStatuses.Vacant;
                    try
                    {
                        var station = dal.printStation(drone.ParcelDelivered);
                        station.ChargeSlots += 1;
                    }
                    catch
                    {
                        throw new NotImplementedException("the drone's parcel did not Delivered");
                    }
                    try
                    {
                        var k = dal.printDroneCharge(drone.Id);
                        dal.GetDroneCharges().ToList().Remove(k);
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
                        c.parcelToCustomer.Add(new Parcel(new CustomerInParcel(c.Id, c.Name)
                            , new CustomerInParcel(dal.printCustomer(item.TargetId).Id, dal.printCustomer(item.TargetId).Name)
                            , (WeightCategories)item.Weight
                            , (Priorities)item.priority
                            , item.Requested
                            , item.scheduled.Value
                            , item.PickedUp.Value
                            , item.Delivered.Value
                            , new DroneInParcel(DronesList.FirstOrDefault(x => x.Id == item.DroneId).Id,
                            DronesList.FirstOrDefault(x => x.Id == item.DroneId).Battery, DronesList.FirstOrDefault(x => x.Id == item.DroneId).LocationNow)));
                    else if (item.TargetId == c.Id)
                        c.parcelFromCustomer.Add(new Parcel(new CustomerInParcel(dal.printCustomer(item.SenderId).Id, dal.printCustomer(item.SenderId).Name)
                           , new CustomerInParcel(c.Id, c.Name)
                           , (WeightCategories)item.Weight
                           , (Priorities)item.priority
                           , item.Requested
                           , item.scheduled.Value
                           , item.PickedUp.Value
                           , item.Delivered.Value
                           , new DroneInParcel(DronesList.FirstOrDefault(x => x.Id == item.DroneId).Id,
                           DronesList.FirstOrDefault(x => x.Id == item.DroneId).Battery, DronesList.FirstOrDefault(x => x.Id == item.DroneId).LocationNow)));
                }
                return c;
            }
            catch (IdException e)
            {
                throw (e);
            }

        }
        public Drone printDrone(int id)
        {

            ParcelInTransference ParcelInTransference;
            var drone = DronesList.FirstOrDefault(x => x.Id == id);
            var parcel = DronesList.FirstOrDefault(x => x.Id == drone.ParcelDelivered);
            if (drone != default)
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
            Parcel p =default;
            try
            {
                var parcel = dal.printParcel(id);
                if (parcel.DroneId != 0)
                {
                    var drone = DronesList.FirstOrDefault(x => x.Id == parcel.DroneId);
                    p = new Parcel(new CustomerInParcel(dal.printCustomer(parcel.SenderId).Id, dal.printCustomer(parcel.SenderId).Name), new CustomerInParcel(dal.printCustomer(parcel.TargetId).Id, dal.printCustomer(parcel.TargetId).Name), (WeightCategories)parcel.Weight, (Priorities)parcel.priority
                     , new DroneInParcel(drone.Id, drone.Battery, drone.LocationNow),parcel.Requested,parcel.DroneId,parcel.scheduled
                    , parcel.PickedUp,parcel.Delivered);
                }
                else
                {
                    p = new Parcel(new CustomerInParcel(dal.printCustomer(parcel.SenderId).Id, dal.printCustomer(parcel.SenderId).Name), new CustomerInParcel(dal.printCustomer(parcel.TargetId).Id, dal.printCustomer(parcel.TargetId).Name),(WeightCategories)parcel.Weight, (Priorities)parcel.priority
                     ,null,parcel.Requested,parcel.DroneId,parcel.scheduled,parcel.PickedUp,parcel.Delivered);
                }
                return p;
            }
            catch
            {
                throw new NotImplementedException();
            }

        }
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
            catch (IdException e)
            {
                throw (e);

            }

        }
        public void sendDroneToStation(int idDrone)
        {
            double min = double.MaxValue;
            double distance = 0;
            var stationHelp = dal.viewStation().FirstOrDefault(x => x.Id == 1);//אתחול
            var drone = DronesList.FirstOrDefault(x => x.Id == idDrone);
            if (drone != default)
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
        }
        public void updateParcelToDrone(int idDrone)
        {
            try
            {
                double Latitude = 0;
                double Longitude = 0;
                 IDAL.DO.Station stationTemp;
                int index = 0;
                int start = 0;
                IDAL.DO.Parcel temp;
                IDAL.DO.Parcel parcelChoise = default;
                int end = dal.viewParcel().Count();
                double minLocation = 99999;
                double tempLocation = 0;
                var p = dal.viewParcel().ToList();
                List<Parcel> parcels = new List<Parcel>();
                List<Parcel> tempParcels = new List<Parcel>();

                var drone1 = DronesList.FirstOrDefault(x => x.Id == idDrone);
                if (drone1 != default)
                {
                    if (drone1.Status == DroneStatuses.Vacant)//האם הרחפן פנוי 
                    {

                        for (int i = 0; i < dal.viewParcel().Count(); i++)
                        {//ממיין את הרשימה מעדיפות גבוהה לנמוכה
                            double min = double.MaxValue;
                            foreach (var station in dal.viewStation())//תחנה קרובה למקבל
                            {
                                if (GetDistanceBetweenTwoLocation(new Location(station.Latitude, station.Longitude), drone1.LocationNow) < min)
                                {
                                    min = GetDistanceBetweenTwoLocation(new Location(station.Latitude, station.Longitude), drone1.LocationNow);
                                   Latitude = station.Latitude;
                                    Longitude = station.Longitude;
                                }
                            }

                            tempLocation =
                                GetDistanceBetweenTwoLocation(new Location(dal.printCustomer(p[i].SenderId).Longitude, dal.printCustomer(p[i].SenderId).Latitude), drone1.LocationNow)
                                + GetDistanceBetweenTwoLocation(new Location(dal.printCustomer(p[i].TargetId).Longitude, dal.printCustomer(p[i].TargetId).Latitude), drone1.LocationNow)
                              + GetDistanceBetweenTwoLocation(new Location(dal.printCustomer(p[i].TargetId).Longitude, dal.printCustomer(p[i].TargetId).Latitude), new Location(Longitude, Latitude));
                            //tempLocation שומר את כל הדרך שעל הרחפן לעבור, כדי להעביר חבילה מלקוח למקבל ולהגיע לתחנה הקרובה לטעינה (כדי לבדוק אם יש לו מספיק בטריה
                            if (tempLocation <= drone1.Battery * ChargingRate)//בודק האם קיימת מספיק בטריה להמשך הדרך
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
                                new Location(dal.printCustomer(p[i].SenderId).Longitude, dal.printCustomer(p[i].SenderId).Latitude)
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
            var parcels = viewParcel();
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
        public void PackageCollectionByDrone(int idDrone)
        {
            try
            {
                var pacelsL = dal.viewParcel().ToList();
                var drone = dal.printDrone(idDrone);
                DroneToList getDrone = DronesList.FirstOrDefault(x => x.Id == idDrone);
                var itemParcel = dal.printParcel(getDrone.ParcelDelivered);
                for (int i = 0; i < dal.viewParcel().Count(); i++)
                {

                    if (pacelsL[i].DroneId == idDrone && pacelsL[i].PickedUp.Equals(default))
                    {
                        getDrone.Battery = getDrone.Battery - (GetDistanceBetweenTwoLocation(getDrone.LocationNow,
                             new Location(dal.printCustomer(pacelsL[i].SenderId).Longitude, dal.printCustomer(pacelsL[i].TargetId).Latitude))) * ChargingRate;
                        getDrone.LocationNow = new Location(dal.printCustomer(pacelsL[i].SenderId).Longitude, dal.printCustomer(pacelsL[i].TargetId).Latitude);
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
                throw new NotImplementedException(e);
            }
        }
        public void DeliveryOfAParcelByDrone(int idDrone)
        {
            try
            {

                var parcels = dal.viewParcel().ToList();
                var drone = dal.printDrone(idDrone);
                DroneToList getDrone = DronesList.FirstOrDefault(x => x.Id == idDrone);
                var itemParcel = dal.printParcel(getDrone.ParcelDelivered);
                for (int i = 0; i < dal.viewParcel().Count(); i++)
                {
                    if (parcels[i].DroneId == idDrone && !parcels[i].PickedUp.Equals(default) && parcels[i].Delivered.Equals(default))
                    {
                        getDrone.Battery = getDrone.Battery - (GetDistanceBetweenTwoLocation(getDrone.LocationNow,
                             new Location(dal.printCustomer(parcels[i].TargetId).Longitude, dal.printCustomer(parcels[i].TargetId).Latitude))) * ChargingRate;
                        getDrone.LocationNow = new Location(dal.printCustomer(parcels[i].SenderId).Longitude, dal.printCustomer(parcels[i].SenderId).Latitude);
                        getDrone.Status = DroneStatuses.Vacant;
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
        public IEnumerable<StationToList> viewStation()
        {
            var stations = dal.viewStation();
            List<StationToList> s = new List<StationToList>();
            foreach (var item in stations)
            {
                int count = 0;
                foreach (var item1 in dal.GetDroneCharges())
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
        public IEnumerable<DroneToList> viewDrone()
        {
            return new List<DroneToList>(DronesList);
        }
        public IEnumerable<CustomerToList> viewCustomer()
        {
            var customers = dal.viewCustomer();
            List<CustomerToList> c = new List<CustomerToList>();
            int countParcelProvided = 0;//חבילות שנשלחו וסופקו
            int countParcelNotProvided = 0;//מונה החבילות שנשלחו ולא סופקו
            int countGetParcels = 0;//חבילות שקיבלתי
            foreach (var item in customers)
            {
                foreach (var item1 in dal.viewParcel())
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
                        countGetParcels++;
                    }

                }
                c.Add(new CustomerToList(item.Id, item.Name, item.Phone, countParcelProvided, countParcelNotProvided, countGetParcels, countParcelNotProvided + countParcelProvided));//צריך להביא
            }
            return c;
        }
        public IEnumerable<ParcelToList> viewParcel()
        {
            var parcels = dal.viewParcel();
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
                p.Add(new ParcelToList(item.Id, item.SenderId, item.TargetId, (WeightCategories)item.Weight, (Priorities)item.priority, temp));
            }
            return p;
        }
        public IEnumerable<StationToList> stationWithAvailableStands()
        {
            List<StationToList> stands = new List<StationToList>();
            var stations = viewStation();
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
                var Drone = dal.printDrone(Id);
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
            catch (IdException e)
            {
                throw (e);
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
            catch (IdException e)
            {
                throw (e);
            }
        }

    }
}


