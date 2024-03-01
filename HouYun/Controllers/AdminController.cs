using HouYun.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouYun.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IVideoRepository _videoRepository;

        public AdminController(IUserRepository userRepository, IVideoRepository videoRepository)
        {
            _userRepository = userRepository;
            _videoRepository = videoRepository;

        }

        public IActionResult Index()
        {
            return View();
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
            return View("UserManagement");
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
            return View("VideoManagement");
        }
    }
}
