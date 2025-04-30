using Traveler.DTOs;
using Traveler.Models.Entities;

namespace Traveler.Interfaces
{
    public interface IUserRepository
    {
        public bool AddUser(User user);
        public bool UserExists(User user);
        public User? GetUser(string email, string password);
    }
}
