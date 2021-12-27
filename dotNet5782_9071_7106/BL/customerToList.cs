using System;
using System.Collections.Generic;
using System.Text;

namespace IBL.BO
{
    class customerToList
    {
        public int Identity { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int ParcelSendAndprovided { get; set; }
        public int ParcelSendAndNotprovided { get; set; }
        public int NumberGetParcel { get; set; }
        public int NumberParcelTOCustomer { get; set; }
        public customerToList(int identity, string name, string phone, int ParcelSendAndprovided, int ParcelSendAndNotprovided, int numberGetParcel, int numberParcelTOCustomer)
        {
            this.Identity = identity;
            this.Name = name;
            this.Phone = phone;
            this.ParcelSendAndprovided = ParcelSendAndprovided;
            this.ParcelSendAndNotprovided = ParcelSendAndNotprovided;
            this.NumberGetParcel = numberGetParcel;
            this.NumberParcelTOCustomer = numberParcelTOCustomer;
        }

        public override string ToString()
        {
            return "Identity:"+ Identity+ " Name:"+ Name+ " Phone:"+ Phone+ " ParcelSendAndprovided:"+ ParcelSendAndprovided+ " ParcelSendAndNotprovided:"+ ParcelSendAndNotprovided
                + " NumberGetParcel:"+ NumberGetParcel+ " NumberParcelTOCustomer:"+ NumberParcelTOCustomer;
        }
    }
}
