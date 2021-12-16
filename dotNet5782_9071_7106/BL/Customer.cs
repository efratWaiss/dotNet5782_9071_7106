using System;

namespace IBL.BO

{
    public struct Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        //public double Longitude { get; set; }
        //public double Latitude { get; set; }
        public Location Location { get; set; }
        public List<Parcel> parcelFromCustomer { get; set; }
        public List<Parcel> parcelToCustomer { get; set; }
        public override string ToString()
        {
            return "id:" + Id + " Name:" + Name + " Phone:" + Phone + " Location" + Location + " parcelFromCustomer:" + parcelFromCustomer+ " parcelToCustomer:"+ parcelToCustomer;
        }
        public Customer(int Id, string Name, string Phone, Location Location, List<Parcel> parcelFromCustomer, List<Parcel> parcelToCustomer)
        {
            this.Id = Id;
            this.Name = Name;
            this.Phone = Phone;
            this.Location = Location;
            this.parcelFromCustomer = parcelFromCustomer;
            this.parcelToCustomer = parcelToCustomer;
        }

    }

}
