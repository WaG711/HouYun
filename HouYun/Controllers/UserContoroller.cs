using Microsoft.AspNetCore.Mvc;
using HouYun.IRepositories;
using System.Security.Claims;
using HouYun.ViewModels.forUser;
using Microsoft.AspNetCore.Authorization;

namespace HouYun.Controllers.UserContoller
{
    [Authorize(Roles = "Admin,User")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _userRepository.Logout();
            return RedirectToAction("", "Video");
        }

        [HttpGet]
        public IActionResult ChangePassword()
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
            var result = await _userRepository.ChangeUserPassword(userId, model.OldPassword, model.NewPassword);

            if (result)
            {
                return RedirectToAction("", "Video");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Не удалось изменить пароль");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult ChangeUsername()
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
            var result = await _userRepository.ChangeUsername(userId, model.NewUsername, model.Password);

            if (result)
            {
                return RedirectToAction("", "Video");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Не удалось изменить никнейм. Возможно, имя уже занято или указан неверный пароль.");
                return View(model);
            }
        }
    }
}
