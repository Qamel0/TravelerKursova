using Traveler.DTOs;
using Traveler.Models.Entities;

namespace Traveler.Interfaces
{
    public interface IUserService
    {
        public bool AddUser(User user);
        public bool UserExists(User user);
    }
}
