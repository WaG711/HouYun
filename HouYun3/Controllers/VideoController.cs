using HouYun3.IRepositories;
using HouYun3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Security.Claims;

namespace HouYun3.Controllers
{
    public class VideoController : Controller
    {
        private readonly IVideoRepository _videoRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISearchHistoryRepository _searchHistoryRepository;

        private readonly IWebHostEnvironment _appEnvironment;

        public VideoController(IVideoRepository videoRepository, ICategoryRepository categoryRepository, IUserRepository userRepository, ISearchHistoryRepository searchHistoryRepository, IWebHostEnvironment appEnvironment)
        {
            _videoRepository = videoRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _searchHistoryRepository = searchHistoryRepository;

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

            return View(video);
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
            var currentUser = await _userRepository.GetUserById(int.Parse(userId));

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
            var user = await _userRepository.GetUserById(int.Parse(userId));

            await _searchHistoryRepository.AddSearchHistory(new SearchHistory
            {
                User = user,
                SearchQuery = searchTerm
            });

            var lastSearches = await _searchHistoryRepository.GetSearchHistoryByUserId(int.Parse(userId));

            var searchResults = await _videoRepository.SearchVideosByTitle(searchTerm);


            ViewData["LastSearches"] = lastSearches;

            return View("Index", new VideoViewModel { Videos = searchResults, CategoryName = "Search Results" });
        }
    }
}
