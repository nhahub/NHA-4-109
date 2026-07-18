namespace PresentationLayer.DTOs.Recommendations
{
    public class PropertyRecommendationDTO
    {
        public Guid PropertyId { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal? Price { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public int? Bedrooms { get; set; }
        public int? Bathrooms { get; set; }
        public string? PropertyType { get; set; }
        public List<string>? Images { get; set; }
        public double Score { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}