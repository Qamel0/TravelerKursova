using System.ComponentModel.DataAnnotations;

namespace Traveler.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Це поле є необхідним")]
        [EmailAddress(ErrorMessage = "Введіть коректний Email")]
        public string Email { get; set; } = null!;
        
        [Required(ErrorMessage = "Це поле є необхідним")]
        public string Password { get; set; } = null!;
    }
}
