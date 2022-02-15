﻿using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    partial class BL : IBL
    {
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
            return new List<DroneToList>(DronesList);//TODO: droneList
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
                BO.ParcelStatsus temp;
                List<ParcelToList> p = new List<ParcelToList>();
                foreach (var item in parcels)
                {
                    if (!item.PickedUp.Equals(null))//נאסף=נאסף
                    {
                        temp = BO.ParcelStatsus.collected;
                    }
                    else if (!item.Delivered.Equals(null))//נמסר=קשור ל...
                    {
                        temp = BO.ParcelStatsus.associated;
                    }
                    else if (!item.Requested.Equals(null))//מבוקש=נוצר
                    {
                        temp = BO.ParcelStatsus.created;
                    }
                    else if (!item.scheduled.Equals(null))//נמסר=קשור ל
                    {
                        temp = BO.ParcelStatsus.Defined;
                    }
                    else
                    {
                        temp = BO.ParcelStatsus.provided;
                    }


                    p.Add(new ParcelToList(item.Id, new CustomerInParcel(dal.GetCustomer(item.SenderId).Id, dal.GetCustomer(item.SenderId).Name)
                       , new CustomerInParcel(dal.GetCustomer(item.TargetId).Id, dal.GetCustomer(item.TargetId).Name)
                         , (BO.WeightCategories)item.Weight
                         , (BO.Priorities)item.priority
                         , temp));
                }
                return p;
            }
            catch (DO.IdException ex) { throw new BO.NotExistException(ex.Message); }
        }
        public IEnumerable<DroneToList> GetListByStatus(DroneStatuses stasus)
        {
            return DronesList.Where(d => d.Status == stasus);
        }
        public IEnumerable<DroneToList> GetListByWeight(BO.WeightCategories weight)
        {
            return DronesList.Where(d => d.MaxWeight == weight);
        }
        public IEnumerable<IGrouping<DroneStatuses, DroneToList>> GetListDroneByGroup()
        {
            var t = from drone in DronesList
                    group drone by drone.Status into g
                    select g;
            return t;
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
            catch (DO.IdException ex) { throw new BO.IdException(ex.Message); }
        }
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
            //public IEnumerable<IGrouping<int, ParcelToList>> GetListParceleByGroup()
            //{
            //    var t = from parcel in GetListParcel()
            //            group parcel by parcel.Sender into g
            //            select g;
            //    return t;
            //}
        }

    }
}

