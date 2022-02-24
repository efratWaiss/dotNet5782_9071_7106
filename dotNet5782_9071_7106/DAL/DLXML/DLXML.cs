using DalApi;
using DO;
using System;
using System.Linq;
using System.Xml.Linq;
using System.Runtime.CompilerServices;

namespace DL
{

    sealed partial class DLXML : IDAL
    {
        public static readonly DLXML instance = new DLXML();
        static DLXML() { }
        DLXML() { }

        public static DLXML Instance { get => instance; }
        

        string CustomerPath = @"CustomerXml.xml";
        string DronePath = @"DroneXml.xml";
        string ParcelPath = @"ParcelXml.xml";
        string StationPath = @"StationXml.xml";
        string DroneChargePath = @"DroneChargeXml.xml";
        string ConfigPath = @"ConfigXml.xml";
      
        

    }
}