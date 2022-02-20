using DalApi;
using DO;
using System;
using System.Linq;
using System.Xml.Linq;

namespace DL
{

    sealed partial class DLXML : IDAL
    {
        static readonly DLXML instance = new DLXML();
        static DLXML() { }
        DLXML() { }
        public static DLXML Instance { get => instance; }
        #region DS XML Files

        string CustomerPath = @"CustomerXml.xml"; //XMLSerializer
        string DronePath = @"DroneXml.xml"; //XMLSerializer
        string ParcelPath = @"ParcelXml.xml"; //XMLSerializer
        string StationPath = @"LecturersXml.xml"; //XMLSerializer
        string DroneChargePath = @"DroneChargeXml.xml"; //XMLSerializer

        #endregion

        #region Person
        public DO.Customer GetCustomer(int id)
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(CustomerPath);

            Customer c = (from cer in personsRootElem.Elements()
                          where int.Parse(cer.Element("ID").Value) == id
                          select new Customer()
                          {
                              Id = Int32.Parse(cer.Element("ID").Value),
                              Name = cer.Element("Name").Value,
                              Phone = cer.Element("Phone").Value,
                              Longitude = double.Parse(cer.Element("Longitude").Value),
                              Latitude = double.Parse(cer.Element("Latitude").Value)
                          }
                        ).FirstOrDefault();

            if (c.Equals(default))
                throw new DO.BadPersonIdException(id, $"Customer id not exists in the system: {id}");

            return c;
        }

        public DO.Drone GetDrone(int id)
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(ParcelPath);

            Drone d = (from der in personsRootElem.Elements()
                       where int.Parse(der.Element("ID").Value) == id
                       select new Drone()
                       {
                           Id = Int32.Parse(der.Element("ID").Value),
                           Model = der.Element("Model").Value,
                           MaxWeight = (WeightCategories)Enum.Parse(typeof(WeightCategories), der.Element("MaxWeight").Value)
                       }
                        ).FirstOrDefault();
            if (d.Equals(default))
                throw new DO.BadPersonIdException(id, $" Drone id not exists in the system: {id}");

            return d;

        }

        public DO.Drone GetParcel(int id)
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(DronePath);

            Parcel p = (from per in personsRootElem.Elements()
                        where int.Parse(per.Element("ID").Value) == id
                        select new Parcel()
                        {
                            Id = Int32.Parse(per.Element("ID").Value),
                            SenderId = Int32.Parse(per.Element("ID").Value),
                            TargetId = Int32.Parse(per.Element("ID").Value),
                            Weight = (WeightCategories)Enum.Parse(typeof(WeightCategories), per.Element("MaxWeight").Value),
                            priority = (Priorities)Enum.Parse(typeof(Priorities), per.Element("priority").Value),
                            Requested = DateTime.Parse(per.Element("Requested").Value),
                            DroneId = Int32.Parse(per.Element("DroneId").Value),
                            scheduled = DateTime.Parse(per.Element("Requested").Value),
                            PickedUp = DateTime.Parse(per.Element("Requested").Value),
                            Delivered = DateTime.Parse(per.Element("Requested").Value)

                        }
                        ).FirstOrDefault();
            if (p.Equals(default))
                throw new DO.BadPersonIdException(id, $" Parcel id not exists in the system: {id}");

            return p;

        }
        public DO.Station GetStation(int id)
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(StationPath);

            Station s = (from ser in personsRootElem.Elements()
                         where int.Parse(ser.Element("ID").Value) == id
                         select new Station()
                         {
                             Id = Int32.Parse(ser.Element("ID").Value),
                             Name = ser.Element("Name").Value,
                             Longitude = double.Parse(ser.Element("Longitude").Value),
                             Latitude = double.Parse(ser.Element("Latitude").Value),
                             ChargeSlots = Int32.Parse(ser.Element("ChargeSlots").Value)
                         }
                        ).FirstOrDefault();
            if (s.Equals(default))
                throw new DO.BadPersonIdException(id, $" Station id not exists in the system: {id}");

            return s;

        }

        public DO.DroneCharge GetDroneCharge(int Droneld)
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(DroneChargePath);

            DroneCharge d = (from ser in personsRootElem.Elements()
                         where int.Parse(ser.Element("Droneld").Value) == Droneld
                             select new DroneCharge()
                         {
                             Droneld = Int32.Parse(ser.Element("DroneId").Value),
                             Stationld = Int32.Parse(ser.Element("Stationld").Value)
                             }
                        ).FirstOrDefault();
            if (d.Equals(default))
                throw new DO.BadPersonIdException(Droneld, $" Drone Id not exists in the system: {Droneld}");
            return d;

        }
    }
}