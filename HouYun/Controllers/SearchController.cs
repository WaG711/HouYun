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

        public async Task<IActionResult> SearchResult(string term)
        {
            var searchResults = await _searchHistoryRepository.SearchVideosByTitle(term);

            var viewModel = new SearchViewModel
            {
                SearchTerm = term,
                Videos = searchResults,
            };

            return View(viewModel);
        }
    }
}
