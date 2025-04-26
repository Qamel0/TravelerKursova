using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Traveler.DTOs;
using Traveler.Interfaces;
using Traveler.Models.Entities;
using Traveler.Models.ViewModels;
using Traveler.Services;

namespace Traveler.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _user;
        private readonly IHttpContextAccessor _contextAccessor;

        public AccountController(IMapper mapper, IUserService user, IHttpContextAccessor contextAccessor)
        {
            _mapper = mapper;
            _user = user;
            _contextAccessor = contextAccessor;
        }

        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = _mapper.Map<User>(model);

                bool userAdded = _user.AddUser(user);

                if(userAdded)
                {
                    LoginService.LoginUser(user, _contextAccessor);

                    return RedirectToAction("Stays", "Categories");
                }
                else if(!userAdded)
                {
                    ModelState.AddModelError("Email", "Користувач з таким email вже існує");
                    return View(model);
                }
                else
                {
                    return RedirectToAction("SomethingWrong", "Error");
                }
            }

            return View(model);
        }

        public IActionResult SignIn()
        {
            return View("SignIn");
        }

        [HttpPost]
        public IActionResult SignIn(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User? user = _user.GetUser(model.Email, model.Password);

                if (user != null)
                {
                    LoginService.LoginUser(user, _contextAccessor);

                    return RedirectToAction("Stays", "Categories");
                }
                else
                {
                    ModelState.AddModelError("wrongLog", "Невірний логін чи пароль");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Stays", "Categories");
        }
    }
}
