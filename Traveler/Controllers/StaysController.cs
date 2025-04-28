using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Traveler.Interfaces;
using Traveler.Models.Entities;
using Traveler.Models.ViewModels;
using Traveler.Services;

namespace Traveler.Controllers
{
    public class StaysController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IStayService _stayService;
        private readonly IBytesConverterService<IFormFile> _bytesConverterService;

        public StaysController(IMapper mapper, IStayService stayService, IBytesConverterService<IFormFile> bytesConverterService)
        {
            _mapper = mapper;
            _stayService = stayService;
            _bytesConverterService = bytesConverterService;
        }

        public IActionResult NewPlace()
        {
            return View("NewPlace");
        }

        [HttpPost]
        public IActionResult NewPlace(StaysRegViewModel model)
        {
            if(ModelState.IsValid)
            {
                Stay stay = _mapper.Map<Stay>(model);

                string? userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                if (userId == null)
                {
                    return RedirectToAction("SomethingWrong", "Error");
                }

                stay.UserId = int.Parse(userId);
                stay.StaysPhoto = _bytesConverterService.ConvertToBytes(model.StaysPhoto);

                bool stayAdded = _stayService.AddStay(stay);

                if (stayAdded)
                {
                    TempData["placeRegistered"] = "Місце успішно зареєстроване";
                    return RedirectToAction("Stays", "Categories");
                }
                else if (!stayAdded)
                {
                    ModelState.AddModelError("stayExists", "Вказане житло вже зареєстроване");
                    return View(model);
                }
                /*else
                {
                    return RedirectToAction("SomethingWrong", "Error");
                }*/
            }

            return RedirectToAction("Stays", "Categories");
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
