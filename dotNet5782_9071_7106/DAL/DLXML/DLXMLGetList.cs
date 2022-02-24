using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Runtime.CompilerServices;


namespace DL
{

    sealed partial class DLXML : IDAL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetListCustomer()
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(CustomerPath);

            return (from c in personsRootElem.Elements()
                    select new Customer()
                    {
                        Id = Int32.Parse(c.Element("ID").Value),
                        Name = c.Element("Name").Value,
                        Phone = c.Element("Phone").Value,
                        Longitude = double.Parse(c.Element("Longitude").Value),
                        Latitude = double.Parse(c.Element("Latitude").Value)
                    }
                   );
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetListDrone()
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(DronePath);

            return (from d in personsRootElem.Elements()
                    select new Drone()
                    {
                        Id = Int32.Parse(d.Element("Id").Value),
                        Model = d.Element("Model").Value,
                        MaxWeight = (WeightCategories)Enum.Parse(typeof(WeightCategories), d.Element("MaxWeight").Value)
                    }
                   );


        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetListParcel()
        //    XElement personsRootElem = XMLTools.LoadListFromXMLElement(CustomerPath);

        //    return (from c in personsRootElem.Elements()
        //            select new Customer()
        //{
        //    Id = Int32.Parse(c.Element("ID").Value),
        //                Name = c.Element("Name").Value,
        //                Phone = c.Element("Phone").Value,
        //                Longitude = double.Parse(c.Element("Longitude").Value),
        //                Latitude = double.Parse(c.Element("Latitude").Value)
        //            }
        //           );
        //}
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(ParcelPath);

            return (from p in personsRootElem.Elements()
                    select new Parcel()
                    {
                        Id = Int32.Parse(p.Element("ID").Value),
                        SenderId = Int32.Parse(p.Element("SenderId").Value),
                        TargetId = Int32.Parse(p.Element("TargetId").Value),
                        Weight = (WeightCategories)Enum.Parse(typeof(WeightCategories), p.Element("MaxWeight").Value),
                        priority = (Priorities)Enum.Parse(typeof(Priorities), p.Element("priority").Value),
                        Requested = DateTime.Parse(p.Element("Requested").Value),
                        DroneId = Int32.Parse(p.Element("DroneId").Value),
                        scheduled = DateTime.Parse(p.Element("Requested").Value),
                        PickedUp = DateTime.Parse(p.Element("Requested").Value),
                        Delivered = DateTime.Parse(p.Element("Requested").Value)

                    }

                   );
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetListStation()
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(StationPath);

            return (from s in personsRootElem.Elements()
                    select new Station()
                    {
                        Id = Int32.Parse(s.Element("ID").Value),
                        Name = s.Element("Name").Value.ToString(),
                        Longitude = double.Parse(s.Element("Longitude").Value),
                        Latitude = double.Parse(s.Element("Latitude").Value),
                        ChargeSlots = Int32.Parse(s.Element("ChargeSlots").Value)
                    }
                   );
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> GetListDroneCharges()
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(DroneChargePath);

            return (from d in personsRootElem.Elements()
                    select new DroneCharge()
                    {
                        Droneld = Int32.Parse(d.Element("DroneId").Value),
                        Stationld = Int32.Parse(d.Element("Stationld").Value)
                    }
                   );
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> ParcelNoDrone()
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(ParcelPath);
            var PNoD = (from p in personsRootElem.Elements()
                        where int.Parse(p.Element("DroneId").Value) == 0
                        select p);
            return (from p in PNoD
                    select new Parcel()
                    {
                        Id = Int32.Parse(p.Element("ID").Value),
                        SenderId = Int32.Parse(p.Element("ID").Value),
                        TargetId = Int32.Parse(p.Element("ID").Value),
                        Weight = (WeightCategories)Enum.Parse(typeof(WeightCategories), p.Element("MaxWeight").Value),
                        priority = (Priorities)Enum.Parse(typeof(Priorities), p.Element("priority").Value),
                        Requested = DateTime.Parse(p.Element("Requested").Value),
                        DroneId = Int32.Parse(p.Element("DroneId").Value),
                        scheduled = DateTime.Parse(p.Element("Requested").Value),
                        PickedUp = DateTime.Parse(p.Element("Requested").Value),
                        Delivered = DateTime.Parse(p.Element("Requested").Value)
                    }
                   );
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> StationNoCharge()
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(StationPath);
            var SNoC = (from p in personsRootElem.Elements()
                        where int.Parse(p.Element("ChargeSlots").Value) == 0
                        select p);
            return (IEnumerable<Station>)SNoC;
        }
    }
}
