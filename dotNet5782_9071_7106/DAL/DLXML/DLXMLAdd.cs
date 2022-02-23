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
        public void AddCustomer(Customer c)
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(CustomerPath);

            XElement cust = (from p in personsRootElem.Elements()
                             where int.Parse(p.Element("Id").Value) == c.Id
                             select p).FirstOrDefault();

            if (cust != null)
                throw new BadPersonIdException(c.Id, "Duplicate customer ID");
            XElement personElem = new XElement("Customer", new XElement("ID", c.Id),
                                  new XElement("Name", c.Name),
                                  new XElement("Phone", c.Phone),
                                  new XElement("Longitude", c.Longitude),
                                  new XElement("Latitude", c.Latitude));


            personsRootElem.Add(personElem);

            XMLTools.SaveListToXMLElement(personsRootElem, CustomerPath);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(Drone drone)
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(DronePath);

            XElement dron = (from d in personsRootElem.Elements()
                             where int.Parse(d.Element("Id").Value) == drone.Id
                             select d).FirstOrDefault();

            if (dron != null)
                throw new BadPersonIdException(drone.Id, "Duplicate drone ID");
            XElement personElem = new XElement("Drone", new XElement("Id", drone.Id),
                                  new XElement("Model", drone.Model),
                                  new XElement("MaxWeight", drone.MaxWeight));


            personsRootElem.Add(personElem);

            XMLTools.SaveListToXMLElement(personsRootElem, DronePath);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int AddParcel(Parcel parcel)
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(ParcelPath);
            XElement configRootElem = XMLTools.LoadListFromXMLElement(ConfigPath);
            XElement idElement = configRootElem.Element("ParcelId");
            int id = int.Parse(idElement.Value);

            XElement pars = (from p in personsRootElem.Elements()
                             where int.Parse(p.Element("SenderId").Value) == parcel.SenderId
                             select p).FirstOrDefault();

            if (pars != null)
                throw new BadPersonIdException(parcel.Id, "Duplicate Sender ID");
            XElement personElem = new XElement("Parcel", new XElement("Id", ++id), new XElement("SenderId", parcel.SenderId),
                                  new XElement("TargetId", parcel.TargetId),
                                  new XElement("Weight", parcel.Weight),
                                  new XElement("Priorities", parcel.priority),
                                  new XElement("Requested", parcel.Requested),
                                  new XElement("DroneId", parcel.DroneId),
                                  new XElement("scheduled", parcel.scheduled),
                                  new XElement("PickedUp", parcel.PickedUp),
                                  new XElement("Delivered", parcel.Delivered));

            personsRootElem.Add(personElem);
            idElement.Value = id.ToString();
            XMLTools.SaveListToXMLElement(configRootElem, ConfigPath);
            XMLTools.SaveListToXMLElement(personsRootElem, ParcelPath);
            return id;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStation(Station s)
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(StationPath);

            XElement pars = (from p in personsRootElem.Elements()
                             where int.Parse(p.Element("Id").Value) == s.Id
                             select p).FirstOrDefault();
            if (pars != null)
                throw new DO.BadPersonIdException(s.Id, "Duplicate station ID");
            XElement StationElem = new XElement("Station", new XElement("Id", s.Id),
                                  new XElement("Name", s.Name),
                                  new XElement("Longitude", s.Longitude),
                                  new XElement("Latitude", s.Latitude),
                                  new XElement("ChargeSlots", s.ChargeSlots));

            personsRootElem.Add(StationElem);

            XMLTools.SaveListToXMLElement(personsRootElem, StationPath);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SendDroneToStation(int idDrone, int idStation)

        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(DronePath);
            XElement personsRootElem2 = XMLTools.LoadListFromXMLElement(DroneChargePath);
            XElement dro = (from c in personsRootElem.Elements()
                            where int.Parse(c.Element("Id").Value) == idDrone
                            select c).FirstOrDefault();

            XElement personsRootElem1 = XMLTools.LoadListFromXMLElement(StationPath);
            XElement sta = (from s in personsRootElem1.Elements()
                            where int.Parse(s.Element("Id").Value) == idStation
                            select s).FirstOrDefault();
            if (!dro.Equals(default))
            {
                if (!sta.Equals(default))
                {
                    XElement DroneChargeElem = new XElement("DroneCharge", new XElement("DroneId", idDrone),
                                      new XElement("StationId", idStation));

                    personsRootElem.Add(DroneChargeElem);

                    XMLTools.SaveListToXMLElement(personsRootElem, DroneChargePath);
                }

                else throw new DO.BadPersonIdException(idDrone, $"Drone id not exist in the system");
            }
            else
                throw new DO.BadPersonIdException(idStation, $"Station id not exist in the system");

        }

    }
}
