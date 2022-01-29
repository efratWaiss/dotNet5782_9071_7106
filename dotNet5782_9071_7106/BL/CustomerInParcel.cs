using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class CustomerInParcel
    {
        public int Identity { get; set; }
        public string Name { get; set; }
        public CustomerInParcel(int identity, string name)
        {
            this.Identity = identity;
            this.Name = name;
           
        }
        public override string ToString()
        {
            return "identity:"+ Identity+ " Name:"+ Name;
        }
    }
}
