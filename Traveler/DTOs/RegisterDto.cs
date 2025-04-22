using System.ComponentModel.DataAnnotations;

namespace Traveler.DTOs
{
    public class RegisterDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
