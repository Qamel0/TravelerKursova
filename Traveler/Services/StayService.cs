using Microsoft.EntityFrameworkCore;
using Traveler.Data;
using Traveler.Interfaces;
using Traveler.Models.Entities;

namespace Traveler.Services
{
    public class StayService : IStayService
    {
        private readonly AppDbContext _context;
        public StayService(AppDbContext context)
        {
            _context = context;
        }

        public bool AddStay(Stay stay)
        {
            if (StayExists(stay)) return false;

            _context.Stays.Add(stay);

            return _context.SaveChanges() >= 1;
        }

        public bool StayExists(Stay stay)
        {
            return _context.Stays.Any(
                s => s.Name == stay.Name && s.RoomCount == stay.RoomCount && s.Describe == stay.Describe);
        }
    }
}
