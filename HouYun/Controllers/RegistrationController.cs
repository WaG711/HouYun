﻿using HouYun.IRepositories;
using HouYun.ViewModels.forUser;
using Microsoft.AspNetCore.Mvc;

namespace HouYun.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IUserRepository _userRepository;

        public RegistrationController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userRepository.RegistrationUser(model);
                if (result)
                {
                    return RedirectToAction("", "Video");
                }
                ModelState.AddModelError(string.Empty, "Проверьте введенные данные");
            }
            return View(model);
        }
    }
}
