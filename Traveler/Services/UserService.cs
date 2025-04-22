using Traveler.Data;
using Traveler.DTOs;
using Traveler.Interfaces;
using Traveler.Models.Entities;

namespace Traveler.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public bool AddUser(User user)
        {
            if(UserExists(user)) return false;

            _context.Users.Add(user);

            return _context.SaveChanges() == 1;
        }

        public bool UserExists(User user)
        {
            return _context.Users.Any(u => u.Email == user.Email);
        }
    }
}
