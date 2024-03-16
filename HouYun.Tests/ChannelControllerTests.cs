using HouYun.Controllers;
using HouYun.IRepositories;
using HouYun.Models;
using HouYun.ViewModels.forUser;
using HouYun.ViewModels.forVideo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using Xunit;

namespace HouYun.Tests
{
    public class ChannelControllerTests
    {
        private readonly string _userId = "testUserId";
        private readonly Channel _channel = new Channel { Name = "Test Channel", Description = "Test Description" };

        [Fact]
        public async Task Index_ReturnsView_WhenChannelExists()
        {
            var channelName = "testChannelName";
            var expectedChannel = new Channel { Name = channelName };
            var mockChannelRepository = new Mock<IChannelRepository>();

            mockChannelRepository.Setup(repo => repo.GetChannelByName(channelName))
                .ReturnsAsync(expectedChannel);

            var controller = CreateChannelController(mockChannelRepository.Object);

            var result = await controller.Index(channelName) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(expectedChannel, result.Model);
        }

        [Fact]
        public async Task Index_RedirectsToAction_WhenChannelDoesNotExist()
        {
            var expectedChannel = new Channel { Name = "testChannelName" };
            var mockChannelRepository = new Mock<IChannelRepository>();

            mockChannelRepository.Setup(repo => repo.GetChannelByName(It.IsAny<string>()))
                .ReturnsAsync((Channel)null);

            mockChannelRepository.Setup(repo => repo.GetChannelByUserId(_userId))
                .ReturnsAsync(expectedChannel);

            var controller = CreateChannelController(mockChannelRepository.Object);

            var result = await controller.Index(null) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal(expectedChannel.Name, result.RouteValues["channelName"]);
        }

        [Fact]
        public async Task Add_Get_ReturnsPartialViewWithModel()
        {
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(repo => repo.GetAllCategories())
                .ReturnsAsync(new[] { new Category { CategoryId = Guid.NewGuid(), Name = "TestCategory" } });

            var controller = new ChannelController(null, mockCategoryRepository.Object, null);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, _userId)
                    }))
                }
            };

            var result = await controller.Add() as PartialViewResult;

            Assert.NotNull(result);
            Assert.IsType<AddVideoViewModel>(result.Model);
        }

        [Fact]
        public async Task Add_Post_InvalidModel_ReturnsPartialViewWithModel()
        {
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(repo => repo.GetAllCategories())
                .ReturnsAsync(new[] { new Category { CategoryId = Guid.NewGuid(), Name = "TestCategory" } });

            var controller = new ChannelController(null, mockCategoryRepository.Object, null);
            var model = new AddVideoViewModel();

            controller.ModelState.AddModelError("Title", "Required");

            var result = await controller.Add(model) as PartialViewResult;

            Assert.NotNull(result);
            Assert.IsType<AddVideoViewModel>(result.Model);
        }

        [Fact]
        public async Task Add_Post_ValidModel_ReturnsJsonResult()
        {
            var mockChannelRepository = new Mock<IChannelRepository>();
            mockChannelRepository.Setup(repo => repo.GetChannelIdByUserId(It.IsAny<string>()))
                .ReturnsAsync(Guid.NewGuid());

            var mockVideoRepository = new Mock<IVideoRepository>();

            var controller = CreateChannelControllerVideo(mockChannelRepository.Object, mockVideoRepository.Object);

            var model = new AddVideoViewModel()
            {
                Title = "Title",
                Description = "Description",
                CategoryId = Guid.NewGuid(),
                VideoFile = new FormFile(null, 0, 50L * 1024 * 1024 * 1024, "TestVideoFile", "TestVideo.mp4"),
                PosterFile = new FormFile(null, 0, 5 * 1024 * 1024, "TestPosterFile", "TestPoster.jpg")
            };

            var result = await controller.Add(model) as JsonResult;

            Assert.NotNull(result);
            Assert.True((bool)result.Value.GetType().GetProperty("success").GetValue(result.Value));
        }

        [Fact]
        public async Task Delete_Get_ReturnsPartialViewWithVideos()
        {
            var channelId = Guid.NewGuid();
            var videos = new List<Video> { new Video { VideoId = Guid.NewGuid(), Title = "Test Video" } };

            var mockChannelRepository = new Mock<IChannelRepository>();
            mockChannelRepository.Setup(repo => repo.GetChannelIdByUserId(_userId))
                .ReturnsAsync(channelId);

            var mockVideoRepository = new Mock<IVideoRepository>();
            mockVideoRepository.Setup(repo => repo.GetVideosByChannelId(channelId))
                .ReturnsAsync(videos);

            var controller = CreateChannelControllerVideo(mockChannelRepository.Object, mockVideoRepository.Object);

            var result = await controller.Delete() as PartialViewResult;

            Assert.NotNull(result);
            Assert.IsType<List<Video>>(result.Model);
            var model = result.Model as List<Video>;
            Assert.Equal(videos.Count, model.Count);
        }

        [Fact]
        public async Task Delete_Post_ReturnsJsonResult()
        {
            var id = Guid.NewGuid();
            var mockVideoRepository = new Mock<IVideoRepository>();

            var controller = new ChannelController(null, null, mockVideoRepository.Object);

            var result = await controller.Delete(id) as JsonResult;

            Assert.NotNull(result);
            Assert.True((bool)result.Value.GetType().GetProperty("success").GetValue(result.Value));
        }

        [Fact]
        public async Task Update_Get_ReturnsPartialViewWithModel()
        {
            var mockChannelRepository = SetupMockChannelRepository();

            var controller = CreateChannelController(mockChannelRepository.Object);

            var result = await controller.Update() as PartialViewResult;

            Assert.NotNull(result);
            Assert.IsType<UpdateChannelViewModel>(result.Model);

            var model = result.Model as UpdateChannelViewModel;
            Assert.Equal(_channel.Name, model.ChannelName);
            Assert.Equal(_channel.Description, model.Description);
        }

        [Fact]
        public async Task Update_Post_InvalidModel_ReturnsPartialViewWithModel()
        {
            var mockChannelRepository = new Mock<IChannelRepository>();

            var controller = new ChannelController(mockChannelRepository.Object, null, null);
            var model = new UpdateChannelViewModel();

            controller.ModelState.AddModelError("ChannelName", "Required");

            var result = await controller.Update(model) as PartialViewResult;

            Assert.NotNull(result);
            Assert.IsType<UpdateChannelViewModel>(result.Model);
        }

        [Fact]
        public async Task Update_Post_ValidModel_Failure_ReturnsPartialViewWithModel()
        {
            var mockChannelRepository = SetupMockChannelRepository();

            mockChannelRepository.Setup(repo => repo.UpdateChannel(_channel))
                .ThrowsAsync(new Exception("Failed"));

            var controller = CreateChannelController(mockChannelRepository.Object);

            var model = new UpdateChannelViewModel
            {
                ChannelName = "Updated Channel",
                Description = "Updated Description"
            };

            var result = await controller.Update(model) as PartialViewResult;

            Assert.NotNull(result);
            Assert.IsType<UpdateChannelViewModel>(result.Model);

            var errorMessage = Assert.Single(controller.ModelState[""].Errors);
            Assert.Equal("Failed", errorMessage.ErrorMessage);
        }

        [Fact]
        public async Task Update_Post_ValidModel_Success_ReturnsJsonResult()
        {
            var mockChannelRepository = SetupMockChannelRepository();

            mockChannelRepository.Setup(repo => repo.UpdateChannel(_channel))
                .Returns(Task.CompletedTask);

            var controller = CreateChannelController(mockChannelRepository.Object);

            var model = new UpdateChannelViewModel
            {
                ChannelName = "Updated Channel",
                Description = "Updated Description"
            };

            var result = await controller.Update(model) as JsonResult;

            Assert.NotNull(result);
            Assert.IsType<JsonResult>(result);

            var successProperty = result.Value.GetType().GetProperty("success");
            Assert.NotNull(successProperty);
            Assert.True((bool)successProperty.GetValue(result.Value));
        }

        private Mock<IChannelRepository> SetupMockChannelRepository()
        {
            var mockChannelRepository = new Mock<IChannelRepository>();
            mockChannelRepository
                .Setup(repo => repo.GetChannelByUserId(_userId))
                .ReturnsAsync(_channel);

            return mockChannelRepository;
        }

        private ChannelController CreateChannelControllerVideo(IChannelRepository channelRepository, IVideoRepository videoRepository)
        {
            var controller = new ChannelController(channelRepository, null, videoRepository);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, _userId)
                    }))
                }
            };
            return controller;
        }

        private ChannelController CreateChannelController(IChannelRepository channelRepository)
        {
            var controller = new ChannelController(channelRepository, null, null);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, _userId)
                    }))
                }
            };
            return controller;
        }
    }
}