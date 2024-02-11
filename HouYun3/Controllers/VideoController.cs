using HouYun3.IRepositories;
using HouYun3.Models;
using HouYun3.ViewModels.forVideo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace HouYun3.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class VideoController : Controller
    {
        private readonly IVideoRepository _videoRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IChannelRepository _channelRepository;

        public VideoController(IVideoRepository videoRepository, ICategoryRepository categoryRepository, IChannelRepository channelRepository)
        {
            _videoRepository = videoRepository;
            _categoryRepository = categoryRepository;
            _channelRepository = channelRepository;
        }

        public async Task<IActionResult> Index(string searchTerm, string category)
        {
            IEnumerable<Video> videos;
            IEnumerable<Category> categories;

            categories = await _categoryRepository.GetAllCategories();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var searchResults = await _videoRepository.SearchVideosByTitle(searchTerm);
                videos = searchResults;
            }
            else if (!string.IsNullOrEmpty(category))
            {
                var videosByCategory = await _videoRepository.GetVideosByCategory(category);
                videos = videosByCategory;
            }
            else
            {
                videos = await _videoRepository.GetAllVideos();
            }

            var viewModel = new VideoViewModel
            {
                Videos = videos,
                SelectedCategory = category ?? "All",
                SearchTerm = searchTerm,
                Categories = categories
            };

            return View(viewModel);
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
            var model = new AddVideoViewModel();
            var categories = await _categoryRepository.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddVideoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var channelId = await _channelRepository.GetChannelIdByUserId(userId);

            if (channelId == null)
            {
                return View(model);
            }

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
