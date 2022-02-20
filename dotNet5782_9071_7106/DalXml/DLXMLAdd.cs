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
       

        

        #region Add
        public void AddCustomer(int Id, String Name, String Phone, double Longitude, double Latitude)
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(CustomerPath);

            XElement cust = (from p in personsRootElem.Elements()
                             where int.Parse(p.Element("ID").Value) == Id
                             select p).FirstOrDefault();

            if (cust != null)
                throw new DO.BadPersonIdException(Id, "Duplicate customer ID");
            XElement personElem = new XElement("Person", new XElement("ID", Id),
                                  new XElement("Name", Name),
                                  new XElement("Street", Phone),
                                  new XElement("HouseNumber", Longitude),
                                  new XElement("City", Latitude));

            personsRootElem.Add(personElem);

            XMLTools.SaveListToXMLElement(personsRootElem, CustomerPath);
        }

        public void AddDrone(int Id, String Model, DO.WeightCategories weight, int IdStation)
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(DronePath);

            XElement dron = (from d in personsRootElem.Elements()
                             where int.Parse(d.Element("ID").Value) == Id
                             select d).FirstOrDefault();

            if (dron != null)
                throw new DO.BadPersonIdException(Id, "Duplicate drone ID");
            XElement personElem = new XElement("Person", new XElement("ID", Id),
                                  new XElement("Model", Model),
                                  new XElement("Street", weight),
                                  new XElement("HouseNumber", IdStation));

            personsRootElem.Add(personElem);

            XMLTools.SaveListToXMLElement(personsRootElem, DronePath);
        }

        public void AddParcel(int SenderId, int TargetId, DO.WeightCategories Weight, DO.Priorities Priorities)
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(ParcelPath);

            XElement pars = (from p in personsRootElem.Elements()
                             where int.Parse(p.Element("SenderId").Value) == SenderId
                             select p).FirstOrDefault();

            if (pars != null)
                throw new DO.BadPersonIdException(SenderId, "Duplicate SenderI ID");
            XElement personElem = new XElement("Person", new XElement("ID", SenderId),
                                  new XElement("Model", TargetId),
                                  new XElement("Street", Weight),
                                  new XElement("HouseNumber", Priorities));

            personsRootElem.Add(personElem);

            XMLTools.SaveListToXMLElement(personsRootElem, ParcelPath);
        }

        public void AddStation(int Id, String Name, Location location, int AvailableChargeSlots)
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(ParcelPath);

            XElement pars = (from p in personsRootElem.Elements()
                             where int.Parse(p.Element("Id").Value) == Id
                             select p).FirstOrDefault();

            if (pars != null)
                throw new DO.BadPersonIdException(Id, "Duplicate station ID");
            XElement personElem = new XElement("Person", new XElement("ID", Id),
                                  new XElement("Name", Name),
                                  new XElement("Street", location),
                                  new XElement("HouseNumber", AvailableChargeSlots));

            personsRootElem.Add(personElem);

            XMLTools.SaveListToXMLElement(personsRootElem, StationPath);
        }
    }
}