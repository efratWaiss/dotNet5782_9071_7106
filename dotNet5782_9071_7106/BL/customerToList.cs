using System;
using System.Collections.Generic;
using System.Text;

namespace IBL.BO
{
    class customerToList
    {
        public int identity { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public int ParcelSendAndprovided { get; set; }
        public int ParcelSendAndNotprovided { get; set; }
        public int numberGetParcel { get; set; }
        public int numberParcelTOCustomer { get; set; }
        public customerToList(int identity, string name, string phone, int ParcelSendAndprovided, int ParcelSendAndNotprovided, int numberGetParcel, int numberParcelTOCustomer)
        {
            this.identity = identity;
            this.name = name;
            this.phone = phone;
            this.ParcelSendAndprovided = ParcelSendAndprovided;
            this.ParcelSendAndNotprovided = ParcelSendAndNotprovided;
            this.numberGetParcel = numberGetParcel;
            this.numberParcelTOCustomer = numberParcelTOCustomer;
        }

        //לא לשכוח להוסיף to strin
    }
}
