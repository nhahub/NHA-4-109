using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Classes
{
    public class Message
    {
        public int MessageID { get; set; }

        public string Content { get; set; }
        public int SenderFlag { get; set; }

        public DateTime ReciveDate { get; set; }
        public DateTime? ReadDate { get; set; }
        
        

        public bool IsDeleted { get; set; }
        
        public int OwnerUsserId { get; set; }
        public Owner Owner { get; set; }
        public int tentantUsserId { get; set; }
        public Tentant tentant { get; set; }

    }
}
