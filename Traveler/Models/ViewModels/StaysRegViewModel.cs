namespace Traveler.Models.ViewModels
{
    public class StaysRegViewModel
    {
        public string PhoneNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int RoomCount { get; set; } 
        public string Describe {  get; set; } = null!;
        public IFormFile StaysPhoto { get; set; } = null!;
    }
}
