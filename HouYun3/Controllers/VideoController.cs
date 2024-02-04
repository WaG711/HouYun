using HouYun3.IRepositories;
using HouYun3.Models;
using HouYun3.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace HouYun3.Controllers
{
    public class VideoController : Controller
    {
        private readonly IVideoRepository _videoRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IWebHostEnvironment _appEnvironment;

        public VideoController(IVideoRepository videoRepository, ICategoryRepository categoryRepository, IWebHostEnvironment appEnvironment, IUserRepository userRepository)
        {
            _videoRepository = videoRepository;
            _categoryRepository = categoryRepository;
            _appEnvironment = appEnvironment;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index(string category)
        {
            var categories = await _categoryRepository.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "Name", "Name");

            var allVideos = await _videoRepository.GetAllVideos();

            var model = new VideoViewModel
            {
                Videos = string.IsNullOrEmpty(category) ? allVideos : allVideos.Where(v => v.Category.Name == category),
                CategoryName = category
            };

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var video = await _videoRepository.GetVideoById(id);

            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryRepository.GetAllCategories();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Video model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser = await _userRepository.GetUserById(int.Parse(userId));

            IFormFile videoFile = model.VideoFile;

            if (videoFile != null && videoFile.Length > 0)
            {
                try
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(videoFile.FileName);
                    var filePath = Path.Combine(_appEnvironment.WebRootPath, "videos", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await videoFile.CopyToAsync(stream);
                    }

                    model.FilePath = "/videos/" + fileName;

                    model.User = currentUser;

                    await _videoRepository.AddVideo(model);

                    return RedirectToAction("Index", "Video");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Произошла ошибка при сохранении видеофайла.");

                    if (!string.IsNullOrEmpty(model.FilePath))
                    {
                        System.IO.File.Delete(_appEnvironment.WebRootPath + model.FilePath);
                    }
                }
            }

            ViewBag.Categories = await _categoryRepository.GetAllCategories();
            return View(model);
        }

        public async Task<IActionResult> Delete()
        {
            var allVideos = await _videoRepository.GetAllVideos();
            return View(allVideos);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int videoId)
        {
            await _videoRepository.DeleteVideo(videoId);
            return RedirectToAction("Delete");
        }
    }
}
