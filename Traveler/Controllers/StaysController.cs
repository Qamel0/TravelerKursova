using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
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

        public IActionResult ShowAllPlaces(string sortOrder)
        {
            var stays = _stayService.GetAllStays();

            stays = stays.Where(s => s.Approved != false);

            if (stays.IsNullOrEmpty())
            {
                TempData["staysErrorLog"] = "Помешкань не знайдено";
                return RedirectToAction("Stays", "Categories");
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

        public IActionResult Places(string? city, string? rooms, string sortOrder)
        {
            var stays = _stayService.GetAllStays();

            stays = stays.Where(s => s.Approved != false);

            if(stays.IsNullOrEmpty())
            {
                TempData["staysErrorLog"] = "Помешкань за заданими параметрами не знайдено";
                return RedirectToAction("Stays", "Categories");
            }

            if (!string.IsNullOrEmpty(city))
            {
                stays = stays.Where(s => s.City.Equals(city, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(rooms) && int.TryParse(rooms, out int roomCount))
            {
                if (roomCount < 4)
                {
                    stays = stays.Where(s => s.RoomCount == roomCount);
                }
                else
                {
                    stays = stays.Where(s => s.RoomCount >= roomCount);
                }
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

        public IActionResult AwaitingApproval(string sortOrder)
        {
            var stays = _stayService.GetAllStays();

            stays = stays.Where(s => s.Approved == false);

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

        public IActionResult ApproveStay(int Id)
        {
            Stay? stay = _stayService.GetStayById(Id);

            if (stay != null)
            {
                _stayService.ApproveStay(stay);
            }

            return View("SearchResult", _stayService.GetAllStays());
        }

        public IActionResult RejectStay(int Id)
        {
            Stay? stay = _stayService.GetStayById(Id);

            if (stay != null)
            {
                _stayService.RemoveStay(stay);
            }

            return View("SearchResult", _stayService.GetAllStays().Where(s => s.Approved == false));
        }

        public IActionResult RemoveStay(int Id)
        {
            Stay? stay = _stayService.GetStayById(Id);

            if (stay != null)
            {
                _stayService.RemoveStay(stay);
            }

            return View("SearchResult", _stayService.GetAllStays().Where(s => s.Approved == true));
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
                    TempData["placeRegistered"] = "Місце успішно зареєстроване. Очікуйте проходження модерації";
                    return RedirectToAction("Stays", "Categories");
                }
                else
                {
                    ModelState.AddModelError("stayExists", "Вказане житло вже зареєстроване");
                    return View(model);
                }
            }

            return RedirectToAction("Stays", "Categories");
        }

        public IActionResult CheckAccess()
        {
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                TempData["staysErrorLog"] = "Ви повинні увійти в акаунт, щоб зареєструвати нове місце";
                return RedirectToAction("Stays", "Categories");
            }

            return RedirectToAction("NewPlace", "Stays");
        }
    }
}
