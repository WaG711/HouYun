using HouYun3.IRepositories;
using HouYun3.Models;
using HouYun3.Repositories;
using HouYun3.ViewModels.forVideo;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouYun3.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchHistoryRepository _searchHistoryRepository;
        private readonly IVideoRepository _videoRepository;
        private readonly IChannelRepository _channelRepository;

        public SearchController(ISearchHistoryRepository searchHistoryRepository, IVideoRepository videoRepository, IChannelRepository channelRepository)
        {
            _searchHistoryRepository = searchHistoryRepository;
            _videoRepository = videoRepository;
            _channelRepository = channelRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchTerm)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var channelId = await _channelRepository.GetChannelIdByUserId(userId);

            var searchHistory = new SearchHistory
            {
                ChannelId = channelId,
                SearchQuery = searchTerm
            };

            await _searchHistoryRepository.AddSearchHistory(searchHistory);

            var lastSearches = await _searchHistoryRepository.GetSearchHistoryByChannelId(channelId);

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
