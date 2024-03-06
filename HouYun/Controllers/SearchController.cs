using HouYun.IRepositories;
using HouYun.ViewModels.forVideo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouYun.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class SearchController : Controller
    {
        private readonly ISearchHistoryRepository _searchHistoryRepository;

        public SearchController(ISearchHistoryRepository searchHistoryRepository)
        {
            _searchHistoryRepository = searchHistoryRepository;
        }

        [HttpGet("Search/Term={searchTerm}")]
        public async Task<IActionResult> SearchResult(string searchTerm)
        {
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
