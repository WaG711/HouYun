using HouYun.Controllers.UserContoller;
using HouYun.IRepositories;
using HouYun.Models;
using HouYun.ViewModels.forUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using Xunit;

namespace HouYun.Tests
{
    public class UserControllerTests
    {
        private readonly string _userId = "testUserId";

        [Fact]
        public async Task Logout_RedirectsToVideoIndex()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.Logout())
                .Returns(Task.CompletedTask);

            var controller = new UserController(mockUserRepository.Object, null);

            var result = await controller.Logout() as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Video", result.ControllerName);
            Assert.Equal("", result.ActionName);
        }

        [Fact]
        public void ChangePassword_ReturnsChangePasswordPartialView()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var controller = new UserController(mockUserRepository.Object, null);

            var result = controller.ChangePassword() as PartialViewResult;

            Assert.NotNull(result);
            Assert.Equal("_ChangePasswordPartial", result.ViewName);
        }

        [Fact]
        public async Task ChangePassword_WithValidModel_ReturnsJsonResult()
        {
            var model = new ChangePasswordViewModel
            {
                OldPassword = "oldPassword",
                NewPassword = "newPassword"
            };

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.ChangeUserPassword(_userId, model.OldPassword, model.NewPassword))
                .ReturnsAsync(true);

            var controller = CreateUserController(mockUserRepository.Object);

            var result = await controller.ChangePassword(model) as JsonResult;

            Assert.NotNull(result);
            Assert.True((bool)result.Value.GetType().GetProperty("success").GetValue(result.Value));
        }

        [Fact]
        public async Task ChangePassword_WithInvalidModel_ReturnsChangePasswordPartialView()
        {
            var model = new ChangePasswordViewModel();

            var mockUserRepository = new Mock<IUserRepository>();

            var controller = new UserController(mockUserRepository.Object, null);
            controller.ModelState.AddModelError("OldPassword", "Required");

            var result = await controller.ChangePassword(model) as PartialViewResult;

            Assert.NotNull(result);
            Assert.Equal("_ChangePasswordPartial", result.ViewName);
            Assert.Equal(model, result.Model);
        }

        [Fact]
        public async Task ChangePassword_WithInvalidPassword_ReturnsChangePasswordPartialViewWithModelError()
        {
            var model = new ChangePasswordViewModel
            {
                OldPassword = "oldPassword",
                NewPassword = "newPassword"
            };

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.ChangeUserPassword(_userId, model.OldPassword, model.NewPassword))
                .ReturnsAsync(false);

            var controller = CreateUserController(mockUserRepository.Object);

            var result = await controller.ChangePassword(model) as PartialViewResult;

            Assert.NotNull(result);
            Assert.Equal("_ChangePasswordPartial", result.ViewName);
            Assert.False(controller.ModelState.IsValid);
        }

        [Fact]
        public void ChangeUserName_ReturnsChangeUserNamePartialView()
        {
            var mockUserRepository = new Mock<IUserRepository>();

            var controller = new UserController(mockUserRepository.Object, null);

            var result = controller.ChangeUserName() as PartialViewResult;

            Assert.NotNull(result);
            Assert.Equal("_ChangeUserNamePartial", result.ViewName);
        }

        [Fact]
        public async Task ChangeUserName_WithValidModel_ReturnsJsonResult()
        {
            var model = new ChangeUserNameViewModel
            {
                NewUserName = "newUserName",
                Password = "password"
            };

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.ChangeUserName(_userId, model.NewUserName, model.Password))
                .ReturnsAsync(true);

            var controller = CreateUserController(mockUserRepository.Object);

            var result = await controller.ChangeUserName(model) as JsonResult;

            Assert.NotNull(result);
            Assert.True((bool)result.Value.GetType().GetProperty("success").GetValue(result.Value));
        }

        [Fact]
        public async Task ChangeUserName_WithInvalidModel_ReturnsChangeUserNamePartialView()
        {
            var model = new ChangeUserNameViewModel();

            var mockUserRepository = new Mock<IUserRepository>();

            var controller = new UserController(mockUserRepository.Object, null);
            controller.ModelState.AddModelError("NewUserName", "Required");

            var result = await controller.ChangeUserName(model) as PartialViewResult;

            Assert.NotNull(result);
            Assert.Equal("_ChangeUserNamePartial", result.ViewName);
            Assert.Equal(model, result.Model);
        }

        [Fact]
        public async Task ChangeUserName_WithInvalidPassword_ReturnsChangeUserNamePartialViewWithModelError()
        {
            var model = new ChangeUserNameViewModel
            {
                NewUserName = "newUserName",
                Password = "password"
            };

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.ChangeUserName(_userId, model.NewUserName, model.Password))
                .ReturnsAsync(false);

            var controller = CreateUserController(mockUserRepository.Object);

            var result = await controller.ChangeUserName(model) as PartialViewResult;

            Assert.NotNull(result);
            Assert.Equal("_ChangeUserNamePartial", result.ViewName);
            Assert.False(controller.ModelState.IsValid);
        }

        [Fact]
        public async Task GetUserName_ReturnsOkObjectResult_WithUserName()
        {
            var userName = "Test";

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.GetUserById(_userId))
                              .ReturnsAsync(new User { UserName = userName });

            var controller = CreateUserController(mockUserRepository.Object);

            var result = await controller.GetUserName();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultUserName = Assert.IsType<string>(okResult.Value);
            Assert.Equal(userName, resultUserName);
        }

        private UserController CreateUserController(IUserRepository userRepository)
        {
            var controller = new UserController(userRepository, null);
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
