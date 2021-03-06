using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DL
{

    sealed partial class DLXML : IDAL
    {
        public void AddCustomer(DO.Customer c)
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(CustomerPath);

            XElement cust = (from p in personsRootElem.Elements()
                             where int.Parse(p.Element("ID").Value) == c.Id
                             select p).FirstOrDefault();

            if (cust != null)
                throw new DO.BadPersonIdException(c.Id, "Duplicate customer ID");
            XElement personElem = new XElement("Customer", new XElement("ID", c.Id),
                                  new XElement("Name", c.Name),
                                  new XElement("Phone", c.Phone),
                                  new XElement("Longitude", c.Longitude),
                                  new XElement("Latitude", c.Latitude));


            personsRootElem.Add(personElem);

            XMLTools.SaveListToXMLElement(personsRootElem, CustomerPath);
        }

        public void AddDrone(DO.Drone drone)
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(DronePath);

            XElement dron = (from d in personsRootElem.Elements()
                             where int.Parse(d.Element("ID").Value) == drone.Id
                             select d).FirstOrDefault();

            if (dron != null)
                throw new DO.BadPersonIdException(drone.Id, "Duplicate drone ID");
            XElement personElem = new XElement("Drone", new XElement("ID", drone.Id),
                                  new XElement("Model", drone.Model),
                                  new XElement("MaxWeight", drone.MaxWeight));


            personsRootElem.Add(personElem);

            XMLTools.SaveListToXMLElement(personsRootElem, DronePath);
        }

        public void AddParcel(DO.Parcel parcel)
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(ParcelPath);

            XElement pars = (from p in personsRootElem.Elements()
                             where int.Parse(p.Element("SenderId").Value) == parcel.SenderId
                             select p).FirstOrDefault();
            //     public int Id { get; set; }
            //public int SenderId { get; set; }
            //public int TargetId { get; set; }
            //public WeightCategories Weight { get; set; }
            //public Priorities priority { get; set; }
            //public DateTime? Requested { get; set; }
            //public int DroneId { get; set; }
            //public DateTime? scheduled { get; set; }
            //public DateTime? PickedUp { get; set; }
            //public DateTime? Delivered { get; set; }
            if (pars != null)
                throw new DO.BadPersonIdException(parcel.Id, "Duplicate SenderI ID");
            XElement personElem = new XElement("Parcel", new XElement("SenderId", parcel.SenderId),
                                  new XElement("TargetId", parcel.TargetId),
                                  new XElement("Weight", parcel.Weight),
                                  new XElement("Priorities", parcel.priority),
                                  new XElement("Requested", parcel.Requested),
                                  new XElement("DroneId", parcel.DroneId),
                                  new XElement("scheduled", parcel.scheduled),
                                  new XElement("PickedUp", parcel.PickedUp),
                                  new XElement("Delivered", parcel.Delivered));

            personsRootElem.Add(personElem);

            XMLTools.SaveListToXMLElement(personsRootElem, ParcelPath);
        }

        public void AddStation(DO.Station s)
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(ParcelPath);

            XElement pars = (from p in personsRootElem.Elements()
                             where int.Parse(p.Element("Id").Value) == s.Id
                             select p).FirstOrDefault();
            if (pars != null)
                throw new DO.BadPersonIdException(s.Id, "Duplicate station ID");
            XElement StationElem = new XElement("Station", new XElement("Id", s.Id),
                                  new XElement("Name", s.Name),
                                  new XElement("Longitude", s.Longitude),
                                  new XElement("Latitude", s.Latitude),
                                  new XElement("AvailableChargeSlots", s.ChargeSlots));

            personsRootElem.Add(StationElem);

            XMLTools.SaveListToXMLElement(personsRootElem, StationPath);
        }

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
