using HouYun3.IRepositories;
using HouYun3.Models;
using HouYun3.ViewModels;
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
        private readonly ISearchHistoryRepository _searchHistoryRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ILikeRepository _likeRepository;
        private readonly IViewRepository _viewRepository;

        public VideoController(IVideoRepository videoRepository, ICategoryRepository categoryRepository, IUserRepository userRepository, ISearchHistoryRepository searchHistoryRepository,
            ICommentRepository commentRepository, ILikeRepository likeRepository, IViewRepository viewRepository)
        {
            _videoRepository = videoRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _searchHistoryRepository = searchHistoryRepository;
            _commentRepository = commentRepository;
            _likeRepository = likeRepository;
            _viewRepository = viewRepository;
        }

        public async Task<IActionResult> Index(string searchTerm, string category)
        {
            IEnumerable<Video> videos;
            IEnumerable<Category> categories;

            categories = await _categoryRepository.GetAllCategories();

            try
            {
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
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve videos: {ex.Message}");
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
            try
            {
                var video = await _videoRepository.GetVideoById(id);
                if (video == null)
                {
                    return NotFound();
                }
                return View(video);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
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

            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var video = new Video
                {
                    Title = model.Title,
                    Description = model.Description,
                    CategoryId = model.CategoryId,
                    UserId = userId
                };

                await _videoRepository.AddVideo(video, model.VideoFile);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
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
            try
            {
                await _videoRepository.DeleteVideo(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchTerm)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var searchHistory = new SearchHistory
            {
                UserId = userId,
                SearchQuery = searchTerm
            };

            await _searchHistoryRepository.AddSearchHistory(searchHistory);

            var lastSearches = await _searchHistoryRepository.GetSearchHistoryByUserId(userId);

            var searchResults = await _videoRepository.SearchVideosByTitle(searchTerm);

            ViewData["LastSearches"] = lastSearches;

            var viewModel = new VideoViewModel
            {
                Videos = searchResults,
                SearchTerm = "Search Results"
            };

            return View("Index", viewModel);
        }

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLike(int videoId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userRepository.GetUserByIdAsync(userId);
            var video = await _videoRepository.GetVideoByIdAsync(videoId);

            if (user != null && video != null)
            {
                var like = new Like
                {
                    User = user,
                    Video = video
                };

                await _likeRepository.AddLikeAsync(like);
            }

            return RedirectToAction("Details", new { id = videoId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveLike(int videoId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var video = await _videoRepository.GetVideoByIdAsync(videoId);

            if (userId != null && video != null)
            {
                var likes = await _likeRepository.GetLikesByVideoIdAsync(videoId);
                var likeToRemove = likes.FirstOrDefault(l => l.UserId == userId);

                if (likeToRemove != null)
                {
                    await _likeRepository.DeleteLikeAsync(likeToRemove.LikeId);
                }
            }

            return RedirectToAction("Details", new { id = videoId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(int videoId, string commentText)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userRepository.GetUserByIdAsync(userId);
            var video = await _videoRepository.GetVideoByIdAsync(videoId);

            if (user != null && video != null && !string.IsNullOrWhiteSpace(commentText))
            {
                var comment = new Comment
                {
                    Text = commentText,
                    User = user,
                    Video = video
                };

                await _commentRepository.AddCommentAsync(comment);
            }

            return RedirectToAction("Details", new { id = videoId });
        }*/
    }
}
