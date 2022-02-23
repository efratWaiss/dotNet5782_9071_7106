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
    sealed partial class DLXML : DalApi.IDAL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateParcelToDrone(int idDrone, int idParcel)
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(ParcelPath);
            XElement personsRootElem1 = XMLTools.LoadListFromXMLElement(DronePath);

            XElement par = (from p in personsRootElem.Elements()
                            where int.Parse(p.Element("idParcel").Value) == idParcel
                            select p).FirstOrDefault();
            XElement dro = (from d in personsRootElem1.Elements()
                            where int.Parse(d.Element("Id").Value) == idDrone
                            select d).FirstOrDefault();
            if (!dro.Equals(default))
            {
                if (!par.Equals(default))
                {
                    par.Element("DroneId").Value = idDrone.ToString();

                    XMLTools.SaveListToXMLElement(personsRootElem, ParcelPath);
                }
                else
                    throw new DO.BadPersonIdException(idParcel, $"parcel id not exist in the system");

            }
            else
                throw new DO.BadPersonIdException(idDrone, $"drone id not exist in the system ");
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateStationDetails(int IdStation, String? NameStation, int? ChargeSlots)
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(StationPath);


            XElement sta = (from s in personsRootElem.Elements()
                            where int.Parse(s.Element("Id").Value) == IdStation
                            select s).FirstOrDefault();


            if (!sta.Equals(default))
            {
                sta.Element("Name").Value = NameStation.ToString();
                sta.Element("ChargeSlots").Value = ChargeSlots.ToString();

                XMLTools.SaveListToXMLElement(personsRootElem, StationPath);
            }
            else
                throw new DO.BadPersonIdException(IdStation, $"Station id not exist in the system");

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomerDetails(int IdCustomer, String? Name, String? Phone)
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(CustomerPath);
            XElement cas = (from c in personsRootElem.Elements()
                            where int.Parse(c.Element("Id").Value) == IdCustomer
                            select c).FirstOrDefault();


            if (!cas.Equals(default))
            {
                cas.Element("Name").Value = Name.ToString();
                cas.Element("Phone").Value = Phone.ToString();

                XMLTools.SaveListToXMLElement(personsRootElem, CustomerPath);
            }
            else
                throw new DO.BadPersonIdException(IdCustomer, $"Customer id not exist in the system");

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateParcel(Parcel p)
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(ParcelPath);
            XElement par = (from pa in personsRootElem.Elements()
                            where int.Parse(pa.Element("Id").Value) == p.Id
                            select pa).FirstOrDefault();


            if (!par.Equals(default))
            {
                par.Element("SenderId").Value = p.TargetId.ToString();
                par.Element("TargetId").Value = p.TargetId.ToString();
                par.Element("Weight").Value = p.Weight.ToString();
                par.Element("priority").Value = p.priority.ToString();
                par.Element("Requested").Value = p.Requested.ToString();
                par.Element("scheduled").Value = p.scheduled.ToString();
                par.Element("DroneId").Value = p.DroneId.ToString();
                par.Element("PickedUp").Value = p.PickedUp.ToString();
                par.Element("Delivered").Value = p.Delivered.ToString();
                XMLTools.SaveListToXMLElement(personsRootElem, ParcelPath);
            }

            else
                throw new DO.BadPersonIdException(p.Id, $"parcel id not exist in the system");

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void FreeDrone(int idDrone)
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(DronePath);
            XElement dro = (from d in personsRootElem.Elements()
                            where int.Parse(d.Element("Id").Value) == idDrone
                            select d).FirstOrDefault();


            if (!dro.Equals(default))
            {
                dro.Remove();
                XMLTools.SaveListToXMLElement(personsRootElem, DronePath);
            }

            else
                throw new DO.BadPersonIdException(idDrone, $"drone id not exist in the system");

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void targetId(int idCustomer, int idParcel)

        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(CustomerPath);
            XElement cas = (from c in personsRootElem.Elements()
                            where int.Parse(c.Element("Id").Value) == idCustomer
                            select c).FirstOrDefault();

            XElement personsRootElem1 = XMLTools.LoadListFromXMLElement(ParcelPath);
            XElement par = (from c in personsRootElem1.Elements()
                            where int.Parse(c.Element("Id").Value) == idParcel
                            select c).FirstOrDefault();
            if (!cas.Equals(default))
            {
                if (!par.Equals(default))
                {
                    par.Element("TargetId").Value = idCustomer.ToString();
                    XMLTools.SaveListToXMLElement(personsRootElem, ParcelPath);
                }

                else throw new DO.BadPersonIdException(idParcel, $"parcel id not exist in the system");
            }
            else
                throw new DO.BadPersonIdException(idCustomer, $"Customer id not exist in the system");

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void pickedUpD(int idDrone, DateTime d)

        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(ParcelPath);
            XElement par = (from c in personsRootElem.Elements()
                            where int.Parse(c.Element("DroneId").Value) == idDrone
                            select c).FirstOrDefault();
            if (!par.Equals(default))
            {

                par.Element("PickedUp").Value = DateTime.Now.ToString();
                XMLTools.SaveListToXMLElement(personsRootElem, ParcelPath);
            }

            else throw new DO.BadPersonIdException(idDrone, $"drobe id not exist in the system");
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteParcel(int id)
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(ParcelPath);

            XElement par = (from p in personsRootElem.Elements()
                            where int.Parse(p.Element("ID").Value) == id
                            select p).FirstOrDefault();

            if (par != null)
            {
                par.Remove(); //  Remove par from personsRootElem

                XMLTools.SaveListToXMLElement(personsRootElem, ParcelPath);
            }
            else
                throw new DO.BadPersonIdException(id, $"parcel id not exits in the system: {id}");
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void removeFromDroneCharges(int idDrone, int idStation)
        {
            XElement personsRootElem1 = XMLTools.LoadListFromXMLElement(DronePath);
            XElement personsRootElem2 = XMLTools.LoadListFromXMLElement(StationPath);
            XElement personsRootElem3 = XMLTools.LoadListFromXMLElement(DroneChargePath);

            XElement dro = (from d in personsRootElem1.Elements()
                            where int.Parse(d.Element("idDrone").Value) == idDrone
                            select d).FirstOrDefault();
            XElement sta = (from s in personsRootElem2.Elements()
                            where int.Parse(s.Element("idStation").Value) == idStation
                            select s).FirstOrDefault();
            if (dro.Equals(default) || sta.Equals(default))
            {
                throw new DO.BadPersonIdException(idDrone + " " + idStation + "Drone id or Station id not exits in the system:");
            }
            XElement droC = (from c in personsRootElem3.Elements()
                             where int.Parse(c.Element("idStation").Value) == idStation &&
                                   int.Parse(c.Element("idDrone").Value) == idDrone
                             select c).FirstOrDefault();

            if (droC != null)
            {
                droC.Remove();
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public double[] powerConsumpitionByDrone()
        {
            //XElement config = XMLTools.LoadListFromXMLElement(ConfigPath);
            //XElement availableElement = config.Element("available");
            //XElement lightWeightElement = config.Element("lightWeight");
            //XElement MediumWeightElement = config.Element("MediumWeight");
            //XElement heavyWeightElement = config.Element("heavyWeight");
            //XElement ChargingRateElement = config.Element("ChargingRate");
            //double available = double.Parse(availableElement.Value);
            //double lightWeight = double.Parse(lightWeightElement.Value);
            //double MediumWeight = double.Parse(MediumWeightElement.Value);
            //double heavyWeight = double.Parse(heavyWeightElement.Value);
            //double ChargingRate = double.Parse(ChargingRateElement.Value);
            double[] arr = new double[5];
            //arr[0] = ++available;
            //arr[1] = ++lightWeight;
            //arr[2] = ++MediumWeight;
            //arr[3] = ++heavyWeight;
            //arr[4] = ++ChargingRate;
            //availableElement.Value = available.ToString();
            //lightWeightElement.Value = lightWeight.ToString();
            //MediumWeightElement.Value = MediumWeight.ToString();
            //heavyWeightElement.Value = heavyWeight.ToString();
            //ChargingRateElement.Value = ChargingRate.ToString();
            //XMLTools.SaveListToXMLElement(config, ConfigPath);
            return arr;



        }
    }
}





