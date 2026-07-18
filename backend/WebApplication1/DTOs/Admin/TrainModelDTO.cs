namespace PresentationLayer.DTOs.Admin
{
    public class TrainModelDTO
    {
        public string ModelType { get; set; } = "all";
        public bool ForceRetrain { get; set; } = false;
    }
}