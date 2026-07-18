namespace PresentationLayer.DTOs
{
    public class SearchMessageDTO
    {
        public int MessageID { get; set; }

        public string Content { get; set; }

        public int SenderFlag { get; set; }

        public DateTime ReciveDate { get; set; }

        public DateTime? ReadDate { get; set; }
 
       

        public int OwnerId { get; set; }

        public int TentantId { get; set; }
    }
}
