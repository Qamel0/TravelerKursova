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
        private readonly IStayRepository _stayService;
        private readonly IBytesConverterService<IFormFile> _bytesConverterService;

        public StaysController(IMapper mapper, IStayRepository stayService, IBytesConverterService<IFormFile> bytesConverterService)
        {
            _mapper = mapper;
            _stayService = stayService;
            _bytesConverterService = bytesConverterService;
        }

        public IActionResult NewPlace()
        {
            return View("NewPlace");
        }

        public IActionResult Places(string? city, string? rooms, string sortOrder)
        {
            var stays = _stayService.GetAllStays();

            if (!string.IsNullOrEmpty(city))
            {
                stays = stays.Where(s => s.City.Equals(city, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(rooms) && int.TryParse(rooms, out int roomCount))
            {
                stays = stays.Where(s => s.RoomCount == roomCount);
            }

            stays = sortOrder switch
            {
                "name_asc" => stays.OrderBy(s => s.Name),
                "name_desc" => stays.OrderByDescending(s => s.Name),
                "room_asc" => stays.OrderBy(s => s.RoomCount),
                "room_desc" => stays.OrderByDescending(s => s.RoomCount),
                _ => stays
            };

            return View("SearchResult", stays.ToList());
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
