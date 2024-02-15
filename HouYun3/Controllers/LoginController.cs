using HouYun3.IRepositories;
using HouYun3.ViewModels.forUser;
using Microsoft.AspNetCore.Mvc;

namespace HouYun3.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;

        public LoginController(IUserRepository userRepository)
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
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userRepository.LoginUser(model.UserName, model.Password, model.RememberMe);
                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Неправильный логин и (или) пароль");
            }
            return View(model);
        }
    }
}
