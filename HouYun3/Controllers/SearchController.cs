using HouYun.IRepositories;
using HouYun.Models;
using HouYun.ViewModels.forVideo;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouYun.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchHistoryRepository _searchHistoryRepository;
        private readonly IChannelRepository _channelRepository;

        public SearchController(ISearchHistoryRepository searchHistoryRepository, IChannelRepository channelRepository)
        {
            _searchHistoryRepository = searchHistoryRepository;
            _channelRepository = channelRepository;
        }

        [HttpGet]
        public async Task<IActionResult> SearchResult(string searchTerm)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var channelId = await _channelRepository.GetChannelIdByUserId(userId);

            var existingSearch = await _searchHistoryRepository.GetSearchHistoryByChannelIdAndQuery(channelId, searchTerm);
            if (existingSearch == null)
            {
                var searchHistory = new SearchHistory
                {
                    ChannelId = channelId,
                    SearchQuery = searchTerm
                };

                await _searchHistoryRepository.AddSearchHistory(searchHistory);
            }

            var lastSearches = await _searchHistoryRepository.GetSearchHistoryByChannelId(channelId);

            var searchResults = await _searchHistoryRepository.SearchVideosByTitle(searchTerm);

            var viewModel = new SearchViewModel
            {
                SearchTerm = searchTerm,
                Videos = searchResults,
                LastSearches = lastSearches
            };

            return View(viewModel);
        }
    }
}
