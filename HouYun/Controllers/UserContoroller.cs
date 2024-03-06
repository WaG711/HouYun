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

        public async Task<IActionResult> Logout()
        {
            await _userRepository.Logout();
            return RedirectToAction("", "Video");
        }

        public IActionResult ChangePassword()
        {
            return PartialView("_ChangePasswordPartial");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_ChangePasswordPartial", model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _userRepository.ChangeUserPassword(userId, model.OldPassword, model.NewPassword);

            if (result)
            {
                return Json(new { success = true });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Не удалось изменить пароль");
                return PartialView("_ChangePasswordPartial", model);
            }
        }

        public IActionResult ChangeUserName()
        {
            return PartialView("_ChangeUserNamePartial");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUserName(ChangeUserNameViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_ChangeUserNamePartial", model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _userRepository.ChangeUserName(userId, model.NewUserName, model.Password);

            if (result)
            {
                return Json(new { success = true });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Ошибка при изменении. Возможно, имя уже занято или указан неверный пароль");
                return PartialView("_ChangeUserNamePartial", model);
            }
        }

        public async Task<IActionResult> GetUserName()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userName = await _userRepository.GetUserNameById(userId);

            return Ok(userName);
        }
    }
}
