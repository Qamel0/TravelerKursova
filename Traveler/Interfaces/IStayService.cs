using Traveler.Models.Entities;

namespace Traveler.Interfaces
{
    public interface IStayService
    {
        public bool AddStay(Stay stay);
        public bool StayExists(Stay stay);
    }
}
