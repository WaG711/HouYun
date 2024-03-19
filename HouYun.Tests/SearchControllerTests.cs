using HouYun.Controllers;
using HouYun.IRepositories;
using HouYun.Models;
using HouYun.ViewModels.forVideo;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HouYun.Tests
{
    public class SearchControllerTests
    {
        [Fact]
        public async Task SearchResult_ReturnsViewWithViewModel()
        {
            var term = "test term";
            var searchResults = new List<Video> { new Video(), new Video() };

            var mockSearchHistoryRepository = new Mock<ISearchHistoryRepository>();
            mockSearchHistoryRepository.Setup(repo => repo.SearchVideosByTitle(term))
                .ReturnsAsync(searchResults);

            var controller = new SearchController(mockSearchHistoryRepository.Object);

            var result = await controller.SearchResult(term) as ViewResult;

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            Assert.IsType<SearchViewModel>(result.Model);

            var viewModel = result.Model as SearchViewModel;
            Assert.Equal(term, viewModel.SearchTerm);
            Assert.Equal(searchResults, viewModel.Videos);
        }
    }
}
