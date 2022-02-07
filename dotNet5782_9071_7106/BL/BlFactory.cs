using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace BlApi
{
    public static class BLFactory
    {
        public static IBL GetBL()
        {

            return new BL();

        }
    }
}
//namespace BO
//{
//    public static class BlFactory
//    {
//        public static IBL GetBl(String st)
//        {
//            return BL;
//        }
//    }
//}
