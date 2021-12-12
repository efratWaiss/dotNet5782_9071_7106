using System;

namespace IBL.BO

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
        public Customer(int Id, string Name, string Phone, double Longitude, double Latitude)
        {
            this.Id = Id;
            this.Name = Name;
            this.Phone = Phone;
            this.Longitude = Longitude;
            this.Latitude = Latitude;
        }

    }
}
