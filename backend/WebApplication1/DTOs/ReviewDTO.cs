using System;

namespace PresentationLayer.DTOs
{
    public class ReviewDTO
    {
        public int ReviewID { get; set; }

        public string Content { get; set; }

        public DateTime ReciveDate { get; set; }

        public DateTime?ReadDate { get; set; }

   

        public int PropertyId { get; set; }

        public int TenantId { get; set; }
    }
}