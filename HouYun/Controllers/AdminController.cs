using HouYun.IRepositories;
using HouYun.Models;
using HouYun.ViewModels.forUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouYun.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IVideoRepository _videoRepository;
        private readonly ICategoryRepository _categoryRepository;

        public AdminController(IUserRepository userRepository, IVideoRepository videoRepository, ICategoryRepository categoryRepository, IApplicationRepository applicationRepository)
        {
            _userRepository = userRepository;
            _videoRepository = videoRepository;
            _categoryRepository = categoryRepository;
            _applicationRepository = applicationRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CategoryManagement()
        {
            var categories = await _categoryRepository.GetAllCategories();
            return View(categories);
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory(Category category)
        {
            await _categoryRepository.AddCategory(category);
            return RedirectToAction("CategoryManagement");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveCategory(Guid id)
        {
            await _categoryRepository.DeleteCategory(id);
            return RedirectToAction("CategoryManagement");
        }

        public async Task<IActionResult> UserManagement()
        {
            var users = await _userRepository.GetAllUsers();

            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveUser(string id)
        {
            await _userRepository.DeleteUser(id);
            return RedirectToAction("UserManagement");
        }

        public async Task<IActionResult> GetApplication(Guid id)
        {
            var application = await _applicationRepository.GetApplicationById(id);

            return PartialView("_ApplicationPartial", application);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveApplication(Guid id)
        {
            await _applicationRepository.DeleteApplication(id);
            return Json(new { success = true });
        }

        public async Task<IActionResult> VideoManagement()
        {
            var videos = await _videoRepository.GetAllVideos();

            return View(videos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveVideo(Guid id)
        {
            await _videoRepository.DeleteVideo(id);
            return RedirectToAction("VideoManagement");
        }
    }
}
