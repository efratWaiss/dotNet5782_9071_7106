using BO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace BlApi
{

    partial class BL : IBL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SendDroneToStation(int idDrone)//שולחת רחפן לתחנת טעינה
        {
            lock (dal)
            {
                double min = double.MaxValue;
                double distance = 0;
                var stationHelp = dal.GetListStation().FirstOrDefault(x => x.Id == 1);//אתחול
                var drone = DronesList.FirstOrDefault(x => x.Id == idDrone);
                if (drone != default)
                {
                    double wightDrone = DroneWeight(drone.Id);
                    if (drone.Status == DroneStatuses.Vacant)//אם הרחפן פנוי
                    {
                        foreach (var station in dal.GetListStation())
                        {//תחפש תחנה קרובה ביותר עם עמדות פנויות
                            distance = GetDistanceBetweenTwoLocation(new Location(station.Longitude, station.Latitude), drone.LocationNow);
                            if (distance < min && station.ChargeSlots != 0 && drone.Battery > distance * wightDrone * ChargingRate)
                            {
                                stationHelp = station;
                            }
                        }
                        drone.Battery = drone.Battery - (distance * wightDrone * ChargingRate);//חישוב בטרייה
                        drone.LocationNow = new Location(stationHelp.Longitude, stationHelp.Latitude);
                        drone.Status = DroneStatuses.Maintenance;
                        stationHelp.ChargeSlots -= 1;//מוריד את העמדות הפנויות
                        dal.UpdateStationDetails(stationHelp.Id, stationHelp.Name, stationHelp.ChargeSlots);//מעדכן את נתוני התחנה
                        dal.GetListDroneCharges().ToList().Add(new DO.DroneCharge(drone.Id, stationHelp.Id));//DroneCharge מוסיף 
                    }

                    else
                    {
                        throw new BO.NotImplementedException("this drone's status not vacant ");
                    }
                }
                else
                {
                    throw new BO.NotImplementedException("this Drone's id not exist in the system");
                }
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]

        //TODO:ערכים 0
        public void UpdateParcelToDrone(int idDrone)
        {
            try
            {
                double Latitude = 0;
                double Longitude = 0;
                int index = 0;
                int start = 0;
                DO.Parcel temp;
                DO.Parcel parcelChoise = default;
                lock (dal)
                {
                    int end = dal.GetListParcel().Count();
                    double minLocation = 99999;
                    double tempLocation = 0;
                    var p = dal.GetListParcel().ToList();
                    List<Parcel> parcels = new List<Parcel>();
                    List<Parcel> tempParcels = new List<Parcel>();
                    bool flag = false;//שומר האם יש חבילה שהרחפן עדיין יכול להגיע אליה
                    var drone1 = DronesList.FirstOrDefault(x => x.Id == idDrone);
                    if (drone1 != default)
                    {
                        if (drone1.Status == DroneStatuses.Vacant)//האם הרחפן פנוי
                        {

                            for (int i = 0; i < p.Count(); i++)
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
                                    GetDistanceBetweenTwoLocation(new Location(dal.GetCustomer(p[i].SenderId).Longitude, dal.GetCustomer(p[i].SenderId).Latitude), drone1.LocationNow)//המרחק בין הרחפן לשולח החבילה
                                    + GetDistanceBetweenTwoLocation(new Location(dal.GetCustomer(p[i].TargetId).Longitude, dal.GetCustomer(p[i].TargetId).Latitude), new Location(dal.GetCustomer(p[i].SenderId).Longitude, dal.GetCustomer(p[i].SenderId).Latitude))//המרחק בין השולח לבין המקבל
                                  + GetDistanceBetweenTwoLocation(new Location(dal.GetCustomer(p[i].TargetId).Longitude, dal.GetCustomer(p[i].TargetId).Latitude), new Location(Longitude, Latitude));//המרחק בין מקבל החבילה לבין התחנה הקרובה ביותר אליו

                                //tempLocation שומר את כל הדרך שעל הרחפן לעבור, כדי להעביר חבילה מלקוח למקבל ולהגיע לתחנה הקרובה לטעינה (כדי לבדוק אם יש לו מספיק בטריה
                                if (tempLocation <= drone1.Battery + ChargingRate * DroneWeight(drone1.Id))//בודק האם קיימת מספיק בטריה להמשך הדרך
                                {
                                    flag = true;
                                    if ((drone1.MaxWeight == BO.WeightCategories.Intermediate && (BO.WeightCategories)p[i].Weight != WeightCategories.Liver) || (drone1.MaxWeight == WeightCategories.Easy && (WeightCategories)p[i].Weight == WeightCategories.Easy))
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
                            if (flag == false)
                            {
                                throw new BO.NotImplementedException("the drone has no enough battery");
                            }
                            if (!parcelChoise.Equals(default))
                            {
                                drone1.Status = DroneStatuses.Shipping;
                                drone1.Battery = drone1.Battery - tempLocation * ChargingRate * DroneWeight(drone1.Id);
                                drone1.ParcelDelivered = parcelChoise.Id;
                                parcelChoise.DroneId = drone1.Id;
                                parcelChoise.Delivered = DateTime.Now;
                                dal.UpdateParcel(parcelChoise);

                            }
                        }
                        else
                        {
                            throw new BO.NotImplementedException("the drone is not vacant");
                        }
                    }

                    else
                    {
                        throw new NotExistException("this Drone's id not exist in the system");//TODO:exception
                    }
                   


                }
            }
            catch (DO.IdException ex) { throw new BO.IdException(ex.Message); }

        }//חבילה, יהיה יותר יפה בהמשך לשנות את השדות שהוא מראה
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void CollectionAParcelByDroen(int idDrone)//איסוף חבילה ע"י רחפן 
        {
            try
            {

                bool flag = false;
                lock (dal)
                {
                    var pacelsL = dal.GetListParcel().ToList();
                    var drone = dal.GetDrone(idDrone);//למצוא ברשימת הרחפנים את הרחפן שהזיהוי שלו שווה לזיהוי הרחפן שקיבלנו
                    DroneToList GetListDrone = DronesList.FirstOrDefault(x => x.Id == idDrone);
                    var itemParcel = dal.GetParcel(GetListDrone.ParcelDelivered);//מציאת החבילות שנאס   פו ע"י רחפנים
                    for (int i = 0; i < dal.GetListParcel().Count() && flag == false; i++)
                    {//עבור על רשימת החבילות כל עוד לא נמצאה חבילה ששויכה לרחפן שנשלח 
                     //ועוד לא נאספה על ידו
                        if (pacelsL[i].DroneId == idDrone && pacelsL[i].PickedUp.Equals(default))
                        {
                            flag = true;
                            GetListDrone.Battery = GetListDrone.Battery - (GetDistanceBetweenTwoLocation(GetListDrone.LocationNow,
                            new Location(dal.GetCustomer(pacelsL[i].SenderId).Longitude, dal.GetCustomer(pacelsL[i].SenderId).Latitude))) * ChargingRate * DroneWeight(pacelsL[i].DroneId);
                            //הבטריה של הרחפן מתעדכנת לבטריה הנדרשת לרחפן על מנת לאסוף את החבילה 
                            GetListDrone.LocationNow = new Location(dal.GetCustomer(pacelsL[i].SenderId).Longitude, dal.GetCustomer(pacelsL[i].SenderId).Latitude);
                            //מיקום הרחפן מתעדכן למיקום הלקוח שמקבל את החבילה 
                            itemParcel.PickedUp = DateTime.Now;//עדכון שעת האיסוף לשעה נוכחית
                            itemParcel.Delivered = null;
                            dal.UpdateParcel(itemParcel); //עדכון החבילה עפ"י החבילה שנשלחה
                        }
                    }
                    if (flag == false)
                    {//אם לא מצאת חבילה ששויכה לרחפן זה ועוד לא נאספה זרוק שגיאה
                        throw new BO.NotImplementedException("The package was not associated with this skimmer or the package has already been collected");
                    }
                }
            }
            catch (DO.IdException ex) { throw new BO.IdException(ex.Message); }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeliveryOfAParcelByDrone(int idDrone)//לספק חבילה על ידי רחפן
        {
            try
            {
                //bool flag = false;
                lock (dal)
                {
                    var parcels = dal.GetListParcel().ToList();
                    var drone = dal.GetDrone(idDrone);
                    DroneToList GetListDrone = DronesList.FirstOrDefault(x => x.Id == idDrone);
                    var itemParcel = dal.GetParcel(GetListDrone.ParcelDelivered);
                    //for (int i = 0; i < dal.GetListParcel().Count() && flag == false; i++)
                    //{
                    //if (itemParcel.DroneId == idDrone)
                    //{

                    if (itemParcel.PickedUp != null && itemParcel.Delivered == null)
                    {// מוצא חבילה שמשויכת לרחפןוגם שהחבילה נאספה אך לא סופקה
                        //flag = true;
                        double v = dal.GetCustomer(itemParcel.TargetId).Longitude;
                        double v1 = dal.GetCustomer(itemParcel.TargetId).Latitude;
                        double b = GetDistanceBetweenTwoLocation(GetListDrone.LocationNow,
                        new Location(dal.GetCustomer(itemParcel.TargetId).Longitude, dal.GetCustomer(itemParcel.TargetId).Latitude));
                        GetListDrone.Battery = GetListDrone.Battery - ((GetDistanceBetweenTwoLocation(GetListDrone.LocationNow,
                        new Location(dal.GetCustomer(itemParcel.TargetId).Longitude, dal.GetCustomer(itemParcel.TargetId).Latitude))) * ChargingRate * DroneWeight(idDrone));
                        GetListDrone.LocationNow = new Location(dal.GetCustomer(itemParcel.SenderId).Longitude, dal.GetCustomer(itemParcel.SenderId).Latitude);
                        GetListDrone.Status = DroneStatuses.Vacant;
                        itemParcel.Delivered = DateTime.Now;
                        dal.UpdateParcel(itemParcel);//מעדכן את החבילה בהתאם
                        double temp= GetDistanceBetweenTwoLocation(new Location(dal.GetCustomer(itemParcel.SenderId).Longitude, dal.GetCustomer(itemParcel.SenderId).Latitude),
                            new Location(dal.GetCustomer(itemParcel.TargetId).Longitude, dal.GetCustomer(itemParcel.TargetId).Latitude));
                        GetListDrone.Battery = GetListDrone.Battery - temp * DroneWeight(idDrone) * available;
                    }

                    else
                    {
                        throw new BO.NotImplementedException("The package has not been collected or the package has already been delivered or the skimmer is not associated with any package");
                    }
                }
            }
            catch (DO.IdException ex) { throw new BO.IdException(ex.Message); }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateNameDrone(int Id, String Model)
        {
            try
            {
                lock (dal)
                {
                    var Drone = dal.GetDrone(Id);
                    Drone.Model = Model;
                }
                var Drone1 = DronesList.FirstOrDefault(x => x.Id == Id);
                Drone1.Model = Model;
            }
            catch (DO.IdException ex) { throw new BO.IdException(ex.Message); }

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateStationDetails(int IdStation, String NameStation, int ChargeSlots)
        {
            try
            {
                lock (dal)
                {
                    dal.UpdateStationDetails(IdStation, NameStation, ChargeSlots);
                }
            }
            catch (DO.IdException ex) { throw new IdException(ex.Message); }

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomerDetails(int IdCustomer, String? Name, String? Phone)
        {
            try
            {
                lock (dal)
                {
                    dal.UpdateCustomerDetails(IdCustomer, Name, Phone);

                }

            }
            catch (DO.IdException ex)
            {
                throw new BO.IdException(ex.Message);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateParcelDetails(int Id, BO.WeightCategories w, BO.Priorities p)
        {
            try
            {
                lock (dal)
                {
                    DO.Parcel parcel = dal.GetParcel(Id);
                    parcel.Weight = (DO.WeightCategories)w;
                    parcel.priority = (DO.Priorities)p;
                    dal.UpdateParcel(parcel);
                }
            }
            catch (DO.IdException ex)
            {
                throw new BO.IdException(ex.Message);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void FreeDrone(int idDrone, double timeInCharging)//שחרור רחפן מטעינת בסיס
        {
            lock (dal)
            {
                DO.Station station = default;
                var drone = DronesList.FirstOrDefault(x => x.Id == idDrone);
                if (drone != default)
                {
                    if (drone.Status == DroneStatuses.Maintenance)
                    {//משנה את הסטטוס ואת הבטרייה
                        drone.Battery += timeInCharging * ChargingRate * DroneWeight(drone.Id);
                        drone.Status = DroneStatuses.Vacant;

                        foreach (var sta in dal.GetListStation())
                        {//מחפש את התחנה שבה ממוקם הרחפן
                            if (sta.Latitude == drone.LocationNow.Latitude && sta.Longitude == drone.LocationNow.Longitude)
                            {
                                station = sta;

                            }

                        }

                        if (!station.Equals(default))
                        {//מוסיף עמדה פנויה לתחנה  ומעדכן
                         //ובנוסף מסיר DroneCharge 
                            int ChargeSlots = station.ChargeSlots + 1;
                            UpdateStationDetails(station.Id, station.Name, ChargeSlots);
                            dal.removeFromDroneCharges(idDrone, station.Id);
                        }


                        else
                        {
                            throw new BO.NotExistException("the drone not found in station");
                        }
                    }

                    else
                    {
                        throw new BO.NotImplementedException("the drone's status is not Maintenance");
                    }
                }

                else
                {
                    throw new BO.NotExistException("this id drone not exist in the system");
                }
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteParcel(int id)
        {
            try
            {
                lock (dal) { dal.DeleteParcel(id); }

            }
            catch (DO.NotExistException ex)
            {
                throw new BO.NotExistException(ex.Message);
            }

        }

    }
}
