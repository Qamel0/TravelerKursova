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

        public bool RemoveStay(Stay stay)
        {
            if (!StayExists(stay)) return false;

            _context.Remove(stay);

            return _context.SaveChanges() >= 1;

        }

        public Stay? GetStayById (int id)
        {
            return _context.Stays.FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Stay> GetAllStays()
        {
            return _context.Stays.ToList();
        }

        public bool StayExists(Stay stay)
        {
            return _context.Stays.Any(
                s => s.Name == stay.Name && stay.City == s.City && s.RoomCount == stay.RoomCount 
                && stay.PhoneNumber == s.PhoneNumber || stay.StaysPhoto == s.StaysPhoto);
        }

        public bool ApproveStay(Stay stay)
        {
            stay.Approved = true;

            _context.Update(stay);

            return _context.SaveChanges() >= 1;
        }
    }
}
