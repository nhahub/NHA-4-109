using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.DTOs.Admin
{
    public class StartScrapeDTO
    {
        [Required]
        public string SiteName { get; set; } = string.Empty;

        [Required]
        public string TargetUrl { get; set; } = string.Empty;

        public int MaxPages { get; set; } = 5;
    }
}