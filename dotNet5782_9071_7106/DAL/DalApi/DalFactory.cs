using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DalApi
{
    public static class DalFactory
    {
        public  static IDAL GetDAL(String type)
        {
            switch (type)
            {
                case "DalObject": return DalObject.DalObject.Instance;
                //case "DalXml":return new DalXml();
                case "DalXml": return DL.DLXML.Instance;
                default: throw new NotFoundThisInstance("This instance not found");
            }
            
        }
    }
    
}
