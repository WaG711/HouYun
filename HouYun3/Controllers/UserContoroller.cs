using Microsoft.AspNetCore.Mvc;
using HouYun.IRepositories;
using System.Security.Claims;
using HouYun.ViewModels.forUser;

namespace HouYun.Controllers.UserContoller
{

    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _userRepository.Logout();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _userRepository.ChangeUserPassword(userId.ToString(), model.OldPassword, model.NewPassword);

            if (result)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Не удалось изменить пароль");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ChangeUsername()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUsername(ChangeUsenameViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _userRepository.ChangeUsername(userId.ToString(), model.NewUsername, model.Password);

            if (result)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Не удалось изменить никнейм. Возможно, имя уже занято или указан неверный пароль.");
                return View(model);
            }
        }

    }
}
