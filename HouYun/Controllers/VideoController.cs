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
        public async Task<IActionResult> Index([FromQuery(Name = "category")] string category)
        {
            if (!string.IsNullOrEmpty(category))
            {
                return RedirectToAction(nameof(Index));
            }

            var model = new VideoViewModel
            {
                Videos = await _videoRepository.GetAllVideos(),
                Categories = await _categoryRepository.GetAllCategories()
            };

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