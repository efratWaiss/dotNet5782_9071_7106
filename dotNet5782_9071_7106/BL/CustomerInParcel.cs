using System;
using System.Collections.Generic;
using System.Text;

namespace IBL.BO
{
    class CustomerInParcel
    {
        public int identity { get; set; }
        public string name { get; set; }
        public CustomerInParcel(int identity, string name)
        {
            this.identity = identity;
            this.name = name;
           
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
