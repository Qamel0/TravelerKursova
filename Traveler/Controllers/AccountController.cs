using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Traveler.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View("Register");
        }

        public IActionResult SignIn()
        {
            return View("SignIn");
        }
    }
}
