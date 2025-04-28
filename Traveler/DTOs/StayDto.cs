namespace Traveler.DTOs
{
    public class StayDto
    {
        public string Phone { get; set; } = null!;
        public string StaysName { get; set; } = null!;
        public int RoomCount { get; set; }
        public string Describe { get; set; } = null!;
        public byte[] StaysPhoto { get; set; } = null!;
    }
}
