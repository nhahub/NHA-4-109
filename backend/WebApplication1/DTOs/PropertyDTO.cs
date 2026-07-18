using DataAccessLayer.Classes;

namespace PresentationLayer.DTOs
{
    public class PropertyDTO
    {
        public int ProperyID { get; set; }

        public int NumberOFRooms { get; set; }

        public int RentPrice { get; set; }

        public string Description { get; set; }

        public ICollection<string> ImageUrls { get; set; }

        public int OwnerId { get; set; }

        public int? AdminId { get; set; }

        public ICollection<int> servicesID { get; set; }=new HashSet<int>();

    }
}