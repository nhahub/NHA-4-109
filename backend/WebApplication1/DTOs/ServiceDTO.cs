using System.Collections.Generic;

namespace PresentationLayer.DTOs
{
    public class ServiceDTO
    {
        public int ServiceID { get; set; }

        public string ServiceName { get; set; }

        public string ServiceType { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public ICollection<int> PropertyIds { get; set; } = new List<int>();
    }
}