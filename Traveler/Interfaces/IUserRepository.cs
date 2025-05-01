using Traveler.Models.Entities;

namespace Traveler.Interfaces
{
    public interface IUserRepository
    {
        public bool AddUser(User user);
        public bool UserExists(User user);
        public User? GetUserById(int id);
        public User? GetUser(string email, string password);
    }
}
