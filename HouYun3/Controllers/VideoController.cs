using HouYun3.IRepositories;
using HouYun3.Models;
using HouYun3.Repositories;
using Microsoft.AspNetCore.Hosting;
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

        public async Task<IActionResult> Index(int? categoryId)
        {
            var categories = await _categoryRepository.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "CategoryID", "Name");

            var allVideos = await _videoRepository.GetAllVideos();

            var model = new VideoViewModel
            {
                Videos = categoryId.HasValue ? allVideos.Where(v => v.CategoryID == categoryId.Value) : allVideos,
                CategoryId = categoryId
            };

            return View(model);
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

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryRepository.GetAllCategories();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Video model)
        {
            IFormFile videoFile = model.VideoFile;

            if (ModelState.IsValid)
            {
                try
                {
                    if (videoFile != null && videoFile.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(videoFile.FileName);
                        var filePath = System.IO.Path.Combine("wwwroot", "videos", fileName);

                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await videoFile.CopyToAsync(stream);
                        }

                        model.FilePath = "/videos/" + fileName;
                    }

                    await _videoRepository.AddVideo(model);

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Произошла ошибка при добавлении видео.");
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
