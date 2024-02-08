using HouYun3.IRepositories;
using HouYun3.Models;
using HouYun3.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouYun3.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchHistoryRepository _searchHistoryRepository;
        private readonly IVideoRepository _videoRepository;

        public SearchController(ISearchHistoryRepository searchHistoryRepository, IVideoRepository videoRepository)
        {
            _searchHistoryRepository = searchHistoryRepository;
            _videoRepository = videoRepository;
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
                SearchTerm = searchTerm
            };

            return RedirectToAction("Index", "Video", viewModel);
        }
    }
}
