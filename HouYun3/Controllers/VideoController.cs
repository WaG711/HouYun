using HouYun3.IRepositories;
using HouYun3.Models;
using HouYun3.ViewModels.forVideo;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouYun3.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class VideoController : Controller
    {
        private readonly IVideoRepository _videoRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IChannelRepository _channelRepository;

        public VideoController(IVideoRepository videoRepository, ICategoryRepository categoryRepository,
            IChannelRepository channelRepository)
        {
            _videoRepository = videoRepository;
            _categoryRepository = categoryRepository;
            _channelRepository = channelRepository;
        }

        public async Task<IActionResult> Index(string category)
        {
            var model = new VideoViewModel();

            if (!string.IsNullOrEmpty(category))
            {
                model.Videos = await _videoRepository.GetVideosByCategory(category);
            }
            else
            {
                model.Videos = await _videoRepository.GetAllVideos();
            }

            model.Categories = await _categoryRepository.GetAllCategories();

            return View(model);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var video = await _videoRepository.GetVideoById(id);
            if (video == null)
            {
                return NotFound();
            }
            return View(video);
        }

        public async Task<IActionResult> Add()
        {
            var model = new AddVideoViewModel
            {
                Categories = await _categoryRepository.GetAllCategories(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddVideoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model = new AddVideoViewModel
                {
                    Categories = await _categoryRepository.GetAllCategories(),
                };

                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var channelId = await _channelRepository.GetChannelIdByUserId(userId);

            var video = new Video
            {
                Title = model.Title,
                Description = model.Description,
                CategoryId = model.CategoryId,
                ChannelId = channelId
            };

            await _videoRepository.AddVideo(video, model.VideoFile, model.PosterFile);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var video = await _videoRepository.GetVideoById(id);
            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _videoRepository.DeleteVideo(id);
            return RedirectToAction("Index");
        }
    }
}