using Microsoft.AspNetCore.Mvc;
using HouYun.IRepositories;
using System.Security.Claims;
using HouYun.ViewModels.forUser;
using Microsoft.AspNetCore.Authorization;
using HouYun.Models;

namespace HouYun.Controllers.UserContoller
{
    [Authorize(Roles = "Admin,User")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IApplicationRepository _applicationRepository;

        public UserController(IUserRepository userRepository, IApplicationRepository applicationRepository)
        {
            _userRepository = userRepository;
            _applicationRepository = applicationRepository;
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
                ModelState.AddModelError(string.Empty, "Возможно, имя пользователя уже занято или указан неверный пароль");
                return PartialView("_ChangeUserNamePartial", model);
            }
        }

        public async Task<IActionResult> ChangeRole()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userRepository.GetUserById(userId);

            var model = new ChangeRoleViewModel()
            {
                User = user
            };

            return PartialView("_ChangeRolePartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeRole(ChangeRoleViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userRepository.GetUserById(userId);

            model.User = user;

            if (!ModelState.IsValid)
            {
                return PartialView("_ChangeRolePartial", model);
            }

            var application = new Application()
            {
                IsActive = true,
                FullName = model.FullName,
                PlaceOfWork = model.PlaceOfWork,
                Thesis = model.Thesis,
                UserId = userId
            };

            await _applicationRepository.AddApplication(application);
            return Json(new { success = true });

        }

        public async Task<IActionResult> GetUserName()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userRepository.GetUserById(userId);

            return Ok(user.UserName);
        }
    }
}
