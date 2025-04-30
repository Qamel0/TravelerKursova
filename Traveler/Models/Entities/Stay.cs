using AutoMapper;

namespace Traveler.Models.Entities
{
    public class Stay
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public string PhoneNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
        public int RoomCount { get; set; }
        public string Describe { get; set; } = null!;
        public byte[] StaysPhoto { get; set; } = null!;
        public bool Approved { get; set; } = false;

        public User User { get; set; } = null!;
    }
}
