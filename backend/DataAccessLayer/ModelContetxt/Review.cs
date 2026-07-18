using DataAccessLayer.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.ModelContetxt
{
    public class Review
    {
        public int ReviewID { get; set; }
        public string Content { get; set; }
        public DateTime ReciveDate { get; set; }
        public DateTime? ReadDate { get; set; }
      
        public int PropertyId { get; set; }
        public Property ReviewdProperty { get; set; }

        public bool IsDeleted { get; set; }
        public int TenantId { get; set; }

        public Tentant Reiewer { get; set; }

    }
}
