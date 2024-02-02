using HouYun3.IRepositories;
using HouYun3.Models;
using Microsoft.AspNetCore.Mvc;

namespace HouYun3.Controllers
{
    public class VideoController : Controller
    {
        private readonly IVideoRepository _videoRepository;

        public VideoController(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }

        public async Task<IActionResult> Index()
        {
            var allVideos = await _videoRepository.GetAllVideos();
            return View(allVideos);
        }

        public async Task<IActionResult> FilterByCategory(int categoryId)
        {
            var videosByCategory = await _videoRepository.GetVideosByCategory(categoryId);
            return View("Index", videosByCategory);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Video video)
        {
            if (ModelState.IsValid)
            {
                await _videoRepository.AddVideo(video);
                return RedirectToAction("Index");
            }
            return View(video);
        }

        public async Task<IActionResult> Delete(int videoId)
        {
            await _videoRepository.DeleteVideo(videoId);
            return RedirectToAction("Index");
        }
    }
}
