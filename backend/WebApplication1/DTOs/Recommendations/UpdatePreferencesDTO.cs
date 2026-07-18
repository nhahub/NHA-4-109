namespace PresentationLayer.DTOs.Recommendations
{
    public class UpdatePreferencesDTO
    {
        public List<string>? PreferredCities { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MinBedrooms { get; set; }
    }
}