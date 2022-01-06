using System;
using System.Collections.Generic;
using System.Text;

namespace IBL.BO
{
  public  class CustomerToList
    {
        public int Identity { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int ParcelSendAndprovided { get; set; }
        public int ParcelSendAndNotprovided { get; set; }
        public int NumberGetListParcel { get; set; }
        public int NumberParcelTOCustomer { get; set; }
        public CustomerToList(int identity, string name, string phone, int ParcelSendAndprovided, int ParcelSendAndNotprovided, int numberGetListParcel, int numberParcelTOCustomer)
        {
            this.Identity = identity;
            this.Name = name;
            this.Phone = phone;
            this.ParcelSendAndprovided = ParcelSendAndprovided;
            this.ParcelSendAndNotprovided = ParcelSendAndNotprovided;
            this.NumberGetListParcel = numberGetListParcel;
            this.NumberParcelTOCustomer = numberParcelTOCustomer;
        }

        public override string ToString()
        {
            return "Identity:"+ Identity+ " Name:"+ Name+ " Phone:"+ Phone+ " ParcelSendAndprovided:"+ ParcelSendAndprovided+ " ParcelSendAndNotprovided:"+ ParcelSendAndNotprovided
                + " NumberGetListParcel:"+ NumberGetListParcel+ " NumberParcelTOCustomer:"+ NumberParcelTOCustomer;
        }
    }
}
