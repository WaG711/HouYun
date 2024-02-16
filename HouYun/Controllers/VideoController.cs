using HouYun.IRepositories;
using HouYun.ViewModels.forVideo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouYun.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class VideoController : Controller
    {
        private readonly IVideoRepository _videoRepository;
        private readonly ICategoryRepository _categoryRepository;

        public VideoController(IVideoRepository videoRepository, ICategoryRepository categoryRepository)
        {
            _videoRepository = videoRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet("")]
        [HttpGet("{category?}")]
        public async Task<IActionResult> Index(string category)
        {
            var model = new VideoViewModel
            {
                Categories = await _categoryRepository.GetAllCategories()
            };

            if (!string.IsNullOrEmpty(category) && model.Categories.Any(c => c.Name == category))
            {
                model.Videos = await _videoRepository.GetVideosByCategory(category);
                return View(model);
            }

            model.Videos = await _videoRepository.GetAllVideos();
            return View(model);
        }

        public async Task<IActionResult> Details(Guid videoId)
        {
            var video = await _videoRepository.GetVideoById(videoId);

            if (video == null)
            {
                return NotFound();
            }

            var model = new DetailsViewModel()
            {
                Video = video,
                Videos = await _videoRepository.GetAllVideosExceptId(videoId)
            };

            return View(model);
        }
    }
}