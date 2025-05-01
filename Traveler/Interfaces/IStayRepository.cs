using Traveler.Models.Entities;

namespace Traveler.Interfaces
{
    public interface IStayRepository
    {
        public bool AddStay(Stay stay);
        public bool RemoveStay(Stay stay);
        public Stay? GetStayById(int id);
        public IEnumerable<Stay> GetAllStays();
        public bool StayExists(Stay stay);
        public bool ApproveStay(Stay stay);
    }
}
