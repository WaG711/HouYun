using HouYun.Controllers;
using HouYun.IRepositories;
using HouYun.Models;
using HouYun.ViewModels.forVideo;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HouYun.Tests
{
    public class VideoControllerTests
    {
        [Fact]
        public async Task Index_ReturnsViewWithVideoViewModel_WhenCategoryExists()
        {
            var categoryName = "TestCategory";
            var expectedVideos = new List<Video> { new Video(), new Video() };
            var mockVideoRepository = new Mock<IVideoRepository>();
            var mockCategoryRepository = new Mock<ICategoryRepository>();

            mockCategoryRepository.Setup(repo => repo.GetAllCategories())
                .ReturnsAsync(new List<Category> { new Category { Name = categoryName } });

            mockVideoRepository.Setup(repo => repo.GetVideosByCategory(categoryName))
                .ReturnsAsync(expectedVideos);

            var controller = new VideoController(mockVideoRepository.Object, mockCategoryRepository.Object);

            var result = await controller.Index(categoryName) as ViewResult;
            var model = result.Model as VideoViewModel;

            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal(expectedVideos, model.Videos);
            Assert.Equal(categoryName, model.Categories.FirstOrDefault()?.Name);
        }

        [Fact]
        public async Task Index_ReturnsViewWithVideoViewModel_WhenCategoryDoesNotExist()
        {
            var mockVideoRepository = new Mock<IVideoRepository>();
            var mockCategoryRepository = new Mock<ICategoryRepository>();

            mockCategoryRepository.Setup(repo => repo.GetAllCategories())
                .ReturnsAsync(new List<Category> { new Category() });

            mockVideoRepository.Setup(repo => repo.GetAllVideos())
                .ReturnsAsync(new List<Video> { new Video(), new Video() });

            var controller = new VideoController(mockVideoRepository.Object, mockCategoryRepository.Object);

            var result = await controller.Index(null) as ViewResult;
            var model = result.Model as VideoViewModel;

            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal(2, model.Videos.Count());
            Assert.Equal(1, model.Categories.Count());
        }

        [Fact]
        public async Task Details_ReturnsViewWithDetailsViewModel_WhenVideoExists()
        {
            var videoId = Guid.NewGuid();
            var expectedVideo = new Video();
            var expectedVideos = new[] { new Video(), new Video() };

            var mockVideoRepository = new Mock<IVideoRepository>();
            mockVideoRepository.Setup(repo => repo.GetVideoById(videoId))
                .ReturnsAsync(expectedVideo);

            mockVideoRepository.Setup(repo => repo.GetAllVideosExceptId(videoId))
                .ReturnsAsync(expectedVideos);

            var controller = new VideoController(mockVideoRepository.Object, null);

            var result = await controller.Details(videoId) as ViewResult;
            var model = result.Model as DetailsViewModel;

            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal(expectedVideo, model.Video);
            Assert.Equal(expectedVideos, model.Videos);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenVideoDoesNotExist()
        {
            var videoId = Guid.NewGuid();

            var mockVideoRepository = new Mock<IVideoRepository>();
            mockVideoRepository.Setup(repo => repo.GetVideoById(videoId))
                .ReturnsAsync((Video)null);

            var controller = new VideoController(mockVideoRepository.Object, null);

            var result = await controller.Details(videoId);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
