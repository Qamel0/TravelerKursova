using Microsoft.AspNetCore.Mvc;

namespace Traveler.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult PageUnderConstruction()
        {
            return View("PageUnderConstruction");
        }

        public IActionResult SomethingWrong()
        {
            return View("SomethingWrong");
        }
    }
}
