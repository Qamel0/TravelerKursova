using Microsoft.AspNetCore.Mvc;

namespace Traveler.Controllers
{
    public class StaysController : Controller
    {
        public IActionResult NewPlace()
        {
            return View("NewPlace");
        }

        public IActionResult CheckAccess()
        {
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                TempData["regLog"] = "Ви повинні увійти в акаунт, щоб зареєструвати нове місце";
                return RedirectToAction("Stays", "Categories");
            }

            return RedirectToAction("NewPlace", "Stays");
        }
    }
}
