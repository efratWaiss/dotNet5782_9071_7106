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
                throw new DO.BadPersonIdException(idDrone+" "+idStation+ "Drone id or Station id not exits in the system:");
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


        public double[] powerConsumpitionByDrone()
        {
            XElement personsRootElem1 = XMLTools.LoadListFromXMLElement(DronePath);
            XElement personsRootElem2 = XMLTools.LoadListFromXMLElement(DronePath);
            XElement personsRootElem3 = XMLTools.LoadListFromXMLElement(DronePath);

        }
    }
}
