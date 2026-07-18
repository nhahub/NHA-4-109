using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Classes
{
    public class Owner:User
    { 
        public string BusinessTaxID { get; set; }

        public ICollection<Property> Properties { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
