using HouYun.IRepositories;
using HouYun.ViewModels.forVideo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouYun.Controllers
{
    [Authorize(Roles = "Admin,User")]
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

            /*var lastSearches = await _searchHistoryRepository.GetSearchHistoryByChannelId(channelId);*/

            var searchResults = await _searchHistoryRepository.SearchVideosByTitle(searchTerm);

            var viewModel = new SearchViewModel
            {
                SearchTerm = searchTerm,
                Videos = searchResults,
            };

            return View(viewModel);
        }
    }
}
