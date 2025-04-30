using Traveler.Data;
using Traveler.DTOs;
using Traveler.Interfaces;
using Traveler.Models.Entities;

namespace Traveler.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool AddUser(User user)
        {
            if(UserExists(user)) return false;

            _context.Users.Add(user);

            return _context.SaveChanges() >= 1;
        }

        public User? GetUser(string email, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public bool UserExists(User user)
        {
            return _context.Users.Any(u => u.Email == user.Email);
        }
    }
}
