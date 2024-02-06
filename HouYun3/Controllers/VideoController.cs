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

        public async Task<IActionResult> Index(string category)
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            IEnumerable<Video> videos;

            if (!string.IsNullOrEmpty(category))
            {
                var categoryId = categories.FirstOrDefault(c => c.Name.Equals(category, StringComparison.OrdinalIgnoreCase))?.CategoryId;
                if (categoryId.HasValue)
                {
                    videos = await _videoRepository.GetVideosByCategoryIdAsync(categoryId.Value);
                }
                else
                {
                    videos = Enumerable.Empty<Video>();
                }
            }
            else
            {
                videos = await _videoRepository.GetAllVideosAsync();
            }

            var categoryList = categories.Select(c => new SelectListItem
            {
                Value = c.Name,
                Text = c.Name
            });

            var viewModel = new VideoViewModel
            {
                Categories = categoryList,
                Videos = videos,
                CategoryName = category
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var video = await _videoRepository.GetVideoByIdAsync(id);

            if (video == null)
            {
                return NotFound();
            }

            var comments = await _commentRepository.GetCommentsByVideoIdAsync(video.VideoId);
            var likesCount = await _likeRepository.GetLikesCountByVideoIdAsync(video.VideoId);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isLikedByCurrentUser = userId != null && await _likeRepository.IsUserLikedVideoAsync(video.VideoId, userId);

            var viewModel = new VideoDetailsViewModel
            {
                Video = video,
                Comments = comments,
                LikesCount = likesCount,
                IsLikedByCurrentUser = isLikedByCurrentUser
            };

            return View(viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Upload()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            var model = new UploadViewModel
            {
                Categories = categories
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(UploadViewModel model)
        {
            if (ModelState.IsValid)
            {
                var video = new Video
                {
                    Title = model.Video.Title,
                    Description = model.Video.Description,
                    DurationSeconds = model.Video.DurationSeconds,
                    CategoryId = model.Video.CategoryId,
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };

                await _videoRepository.AddVideoAsync(video, model.Video.VideoFile);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var video = await _videoRepository.GetVideoByIdAsync(id);

            if (video == null)
            {
                return NotFound();
            }

            if (video.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Forbid();
            }

            await _videoRepository.DeleteVideoAsync(id);

            return RedirectToAction("UserVideos");
        }

        public async Task<IActionResult> UserVideos()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userVideos = await _videoRepository.GetVideosByUserIdAsync(userId);

            return View(userVideos);
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

            await _searchHistoryRepository.AddSearchHistoryAsync(searchHistory);

            var lastSearches = await _searchHistoryRepository.GetSearchHistoryByUserIdAsync(userId);

            var searchResults = await _videoRepository.SearchVideosByTitleAsync(searchTerm);

            ViewData["LastSearches"] = lastSearches;

            var viewModel = new VideoViewModel
            {
                Videos = searchResults,
                CategoryName = "Search Results"
            };

            return View("Index", viewModel);
        }

        [HttpPost]
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
        }
    }
}
