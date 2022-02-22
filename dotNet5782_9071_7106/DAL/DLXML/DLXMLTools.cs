﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DL
{
    class XMLTools
    {
        static string dir = @"xml";
        static XMLTools()
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }
        #region SaveLoadWithXElement
        public static void SaveListToXMLElement(XElement rootElem, string filePath)
        {
            try
            {
                rootElem.Save(Path.Combine(dir, filePath));
            }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException($"fail to create xml file: {filePath}", ex);
            }
        }

        public static XElement LoadListFromXMLElement(string filePath)
        {
            try
            {
                if (File.Exists(Path.Combine(dir, filePath)))
                {
                    return XElement.Load(Path.Combine(dir, filePath));
                }
                else
                {
                    XElement rootElem = new XElement(Path.Combine(dir, filePath));
                    rootElem.Save(Path.Combine(dir, filePath));
                    return rootElem;
                }
            }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException($"fail to load xml file: {filePath}", ex);
            }
        }
        #endregion
    }
}