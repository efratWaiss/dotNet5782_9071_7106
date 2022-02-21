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
        public void UpdateParcelToDrone(int idDrone, int idParcel)
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(ParcelPath);

            XElement per = (from p in personsRootElem.Elements()
                            where int.Parse(p.Element("idParcel").Value) == idDrone
                            select p).FirstOrDefault();

            if (per != null)
            {//per.Element("ID").Value = person.ID.ToString();
                per.Element("ID").Value = Customer.Id.ToString();
                per.Element("TargetId").Value = TargetId,
                TargetId = Int32.Parse(p.Element("ID").Value),
                        Weight = (WeightCategories)Enum.Parse(typeof(WeightCategories), p.Element("MaxWeight").Value),
                        priority = (Priorities)Enum.Parse(typeof(Priorities), p.Element("priority").Value),
                        Requested = DateTime.Parse(p.Element("Requested").Value),
                        DroneId = Int32.Parse(p.Element("DroneId").Value),
                        scheduled = DateTime.Parse(p.Element("Requested").Value),
                        PickedUp = DateTime.Parse(p.Element("Requested").Value),
                        Delivered = DateTime.Parse(p.Element("Requested").Value)


                XMLTools.SaveListToXMLElement(personsRootElem, personsPath);
            }
            else
                throw new DO.BadPersonIdException(person.ID, $"bad person id: {person.ID}");
        }
    }
}
