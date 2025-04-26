using Microsoft.AspNetCore.Mvc;

namespace Traveler.Controllers
{
    public class StaysController : Controller
    {
        public IActionResult NewPlace()
        {
            return View("NewPlace");
        }
    }
}
