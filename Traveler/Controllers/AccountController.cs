using AutoMapper;
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

        public AccountController(IMapper mapper, IUserService user)
        {
            _mapper = mapper;
            _user = user;
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

            }

            return View(model);
        }
    }
}
