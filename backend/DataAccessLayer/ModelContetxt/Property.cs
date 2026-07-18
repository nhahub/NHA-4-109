using DataAccessLayer.ModelContetxt;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.ModelContext;

namespace DataAccessLayer.Classes
{
    public class Property : IOwned
    {
       
        public int ProperyID { get; set; }
        public int NumberOFRooms { get; set; }
        public int RentPrice { get; set; }
        public string Description { get; set; }
        public ICollection<string> ImageUrls { get; set; }
        public int? ManageAdminId { get; set; }
        public Admin? ManageAdmin { get; set; }

        public bool IsDeleted { get; set; }
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<Service> Services { get; set; } = new List<Service>();
        

    }
}
