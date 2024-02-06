using HouYun3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using HouYun3.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using HouYun3.IRepositories;

namespace HouYun3.Controllers.UserContoller
{

    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private readonly IWatchLaterRepository _watchLaterRepository;
        private readonly IVideoRepository _videoRepository;
        private readonly IUserRepository _userRepository;

        public UserController(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<UserController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { Email = model.Email, UserName = model.UserName };
                var existingUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

                if (existingUser != null)
                {
                    ModelState.AddModelError(string.Empty, "Этот почтовый адрес уже используется.");
                    return View(model);
                }

                var result = await _userManager.CreateAsync(user, model.Password);


                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
        public async Task<IActionResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("", "");
                }
                ModelState.AddModelError(string.Empty, "Неправильный логин и (или) пароль");

            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(string Username)
        {
            User user = await _userManager.FindByIdAsync(Username);
            if (user == null)
            {
                return NotFound();
            }
            ChangePasswordViewModel model = new ChangePasswordViewModel { UserName = user.UserName};
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            User user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                return RedirectToAction("PasswordChanged");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }
        }

        public class UploadVideos()
        {

        }

    }
}
