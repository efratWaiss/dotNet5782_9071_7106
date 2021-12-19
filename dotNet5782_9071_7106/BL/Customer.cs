using System;
using System.Collections.Generic;

namespace IBL.BO

{
    public class Customer
    {
        public Customer(int id, string name, string phone, Location location, List<Parcel> parcelFromCustomer, List<Parcel> parcelToCustomer)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Location = location;
            this.parcelFromCustomer = parcelFromCustomer;
            this.parcelToCustomer = parcelToCustomer;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Location Location { get; set; }
        public List<Parcel> parcelFromCustomer { get; set; }
        public List<Parcel> parcelToCustomer { get; set; }
        public override string ToString()
        {
            return "id:" + Id + " Name:" + Name + " Phone:" + Phone + " Location" + Location + " parcelFromCustomer:" + parcelFromCustomer+ " parcelToCustomer:"+ parcelToCustomer;
        }
       

    }

}
