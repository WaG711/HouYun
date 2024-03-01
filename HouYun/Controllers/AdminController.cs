using HouYun.IRepositories;
using HouYun.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouYun.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IVideoRepository _videoRepository;
        private readonly ICategoryRepository _categoryRepository;

        public AdminController(IUserRepository userRepository, IVideoRepository videoRepository, ICategoryRepository categoryRepository)
        {
            _userRepository = userRepository;
            _videoRepository = videoRepository;
            _categoryRepository = categoryRepository;
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
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveCategory(Guid id)
        {
            await _categoryRepository.DeleteCategory(id);
            return RedirectToAction("Index");
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
