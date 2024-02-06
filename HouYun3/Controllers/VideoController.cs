using HouYun3.IRepositories;
using HouYun3.Models;
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

        private readonly IWebHostEnvironment _appEnvironment;

        public VideoController(IVideoRepository videoRepository, ICategoryRepository categoryRepository, IUserRepository userRepository, ISearchHistoryRepository searchHistoryRepository,
            ICommentRepository commentRepository, ILikeRepository likeRepository, IViewRepository viewRepository, IWebHostEnvironment appEnvironment)
        {
            _videoRepository = videoRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _searchHistoryRepository = searchHistoryRepository;
            _commentRepository = commentRepository;
            _likeRepository = likeRepository;
            _viewRepository = viewRepository;

            _appEnvironment = appEnvironment;
        }

        public async Task<IActionResult> Index(string category)
        {
            var categories = await _categoryRepository.GetAllCategories();
            var model = new VideoViewModel
            {
                Categories = new SelectList(categories, "Name", "Name"),
                Videos = string.IsNullOrWhiteSpace(category)
                    ? await _videoRepository.GetAllVideos()
                    : (await _videoRepository.GetAllVideos()).Where(v => v.Category.Name.Equals(category, StringComparison.OrdinalIgnoreCase)),
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

            var comments = await _commentRepository.GetCommentsByVideoId(video.VideoId);
            var likesCount = await _likeRepository.GetLikesCountByVideoId(video.VideoId);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isLikedByCurrentUser = userId != null && await _likeRepository.IsUserLikedVideo(video.VideoId, int.Parse(userId));

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
            var currentUser = await _userRepository.GetUserByIdAsync(userId.ToString());

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

        public async Task<IActionResult> DeleteList()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userVideos = await _videoRepository.GetUserVideos(int.Parse(userId));

            return View(userVideos);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = await _videoRepository.GetVideoById(id.Value);

            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _videoRepository.DeleteVideo(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchTerm)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userRepository.GetUserByIdAsync(userId.ToString());

            await _searchHistoryRepository.AddSearchHistory(new SearchHistory
            {
                User = user,
                SearchQuery = searchTerm
            });

            var lastSearches = await _searchHistoryRepository.GetSearchHistoryByUserId(userId.ToString());

            var searchResults = await _videoRepository.SearchVideosByTitle(searchTerm);


            ViewData["LastSearches"] = lastSearches;

            return View("Index", new VideoViewModel { Videos = searchResults, CategoryName = "Search Results" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(int videoId, string commentText)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userRepository.GetUserByIdAsync(userId.ToString());
            var video = await _videoRepository.GetVideoById(videoId);

            if (user != null && video != null && !string.IsNullOrWhiteSpace(commentText))
            {
                var comment = new Comment
                {
                    Text = commentText,
                    User = user,
                    Video = video
                };

                await _commentRepository.AddComment(comment);
            }

            return RedirectToAction("Details", new { id = videoId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLike(int videoId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userRepository.GetUserByIdAsync(userId.ToString());
            var video = await _videoRepository.GetVideoById(videoId);

            if (user != null && video != null)
            {
                var like = new Like
                {
                    User = user,
                    Video = video
                };

                await _likeRepository.AddLike(like);
            }

            return RedirectToAction("Details", new { id = videoId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveLike(int videoId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var video = await _videoRepository.GetVideoById(videoId);

            if (userId != null && video != null)
            {
                var isLikedByUser = await _likeRepository.IsUserLikedVideo(video.VideoId, int.Parse(userId));

                if (isLikedByUser)
                {
                    await _likeRepository.RemoveLike(int.Parse(userId), video.VideoId);
                }
            }

            return RedirectToAction("Details", new { id = videoId });
        }
    }
}
