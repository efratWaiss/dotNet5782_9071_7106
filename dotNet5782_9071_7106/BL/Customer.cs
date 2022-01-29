using System;
using System.Collections.Generic;

namespace BO

{
    public class Customer
    {
        public Customer(int id, string name, string phone, Location location)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Location = location;
            this.parcelFromCustomer = new();
            this.parcelToCustomer = new();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Location Location { get; set; }
        public List<Parcel> parcelFromCustomer { get; set; }
        public List<Parcel> parcelToCustomer { get; set; }
        public override string ToString()
        {
            return "id:" + Id + " Name:" + Name + " Phone:" + Phone + " Location" + Location + " parcelFromCustomer:" + string.Join("," ,parcelFromCustomer)+ " parcelToCustomer:"+ string.Join(",", parcelToCustomer,"\n");
        }
       

    }

}
