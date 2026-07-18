using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.DTOs.Recommendations
{
    public class RecordInteractionDTO
    {
        [Required]
        public Guid PropertyId { get; set; }

        [Required]
        public string InteractionType { get; set; } = string.Empty;

        public double? Rating { get; set; }
    }
}