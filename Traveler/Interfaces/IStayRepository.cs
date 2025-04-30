using Traveler.Models.Entities;

namespace Traveler.Interfaces
{
    public interface IStayRepository
    {
        public bool AddStay(Stay stay);
        public IEnumerable<Stay> GetAllStays();
        public bool StayExists(Stay stay);
    }
}
