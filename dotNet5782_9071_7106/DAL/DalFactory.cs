using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public static class DalFactory
    {
        public  static  IDAL GetDAL(String st)
        {
            return DalObject.st;
        }
    }
}
