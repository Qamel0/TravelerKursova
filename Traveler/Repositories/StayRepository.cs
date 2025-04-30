using Microsoft.EntityFrameworkCore;
using Traveler.Data;
using Traveler.Interfaces;
using Traveler.Models.Entities;

namespace Traveler.Repositories
{
    public class StayRepository : IStayRepository
    {
        private readonly AppDbContext _context;
        public StayRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool AddStay(Stay stay)
        {
            if (StayExists(stay)) return false;

            _context.Stays.Add(stay);

            return _context.SaveChanges() >= 1;
        }

        public IEnumerable<Stay> GetAllStays()
        {
            return _context.Stays.ToList();
        }

        public bool StayExists(Stay stay)
        {
            return _context.Stays.Any(
                s => s.Name == stay.Name && s.RoomCount == stay.RoomCount && s.Describe == stay.Describe);
        }
    }
}
