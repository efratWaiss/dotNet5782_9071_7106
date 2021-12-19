﻿using System;

namespace IDAL
{
    namespace DO
    {
        public struct Customer
	{
            

            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public override string ToString()
            {
                return "id:" + Id + " Name:" + Name + " Phone:" + Phone + " Longitude:" + Longitude + " Latitude:" + Latitude;
            }
            public Customer(int id, string name, string phone, double longitude, double latitude)
            {
                Id = id;
                Name = name;
                Phone = phone;
                Longitude = longitude;
                Latitude = latitude;
            }
        }
}
}