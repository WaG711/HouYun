using HouYun3.IRepositories;
using HouYun3.Models;
using HouYun3.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HouYun3.Controllers
{
    public class VideoController : Controller
    {
        private readonly IVideoRepository _videoRepository;
        private readonly ICategoryRepository _categoryRepository;

        public VideoController(IVideoRepository videoRepository, ICategoryRepository categoryRepository)
        {
            _videoRepository = videoRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var allVideos = await _videoRepository.GetAllVideos();
            var categories = await _categoryRepository.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "CategoryID", "Name");

            return View(allVideos);
        }

        [HttpPost]
        public async Task<IActionResult> FilterByCategory(int categoryId)
        {
            var videos = await _videoRepository.GetVideosByCategory(categoryId);
            return PartialView("_VideoListPartial", videos);
        }

        public async Task<IActionResult> Details(int id)
        {
            var video = await _videoRepository.GetVideo(id);

            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _categoryRepository.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "CategoryID", "Name");

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

            var categories = await _categoryRepository.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "CategoryID", "Name");

            return View(video);
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
