using HouYun.Controllers;
using HouYun.IRepositories;
using HouYun.ViewModels.forUser;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HouYun.Tests
{
    public class LoginControllerTests
    {
        [Fact]
        public async Task Index_WithValidModelAndSuccessfulLogin_RedirectsToVideoAction()
        {
            var model = new LoginViewModel
            {
                UserName = "testuser",
                Password = "testpassword",
                RememberMe = true
            };

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.LoginUser(model.UserName, model.Password, model.RememberMe))
                .ReturnsAsync(true);

            var controller = new LoginController(mockUserRepository.Object);

            var result = await controller.Index(model) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Video", result.ControllerName);
            Assert.Equal("", result.ActionName);
        }

        [Fact]
        public async Task Index_WithInvalidModel_ReturnsViewWithModelError()
        {
            var model = new LoginViewModel();

            var mockUserRepository = new Mock<IUserRepository>();
            var controller = new LoginController(mockUserRepository.Object);

            controller.ModelState.AddModelError("UserName", "Required");

            var result = await controller.Index(model) as ViewResult;

            Assert.NotNull(result);
            Assert.False(controller.ModelState.IsValid);
            Assert.Equal(model, result.Model);
        }

        [Fact]
        public async Task Index_WithInvalidCredentials_ReturnsViewWithModelError()
        {
            var model = new LoginViewModel
            {
                UserName = "testuser",
                Password = "testpassword",
                RememberMe = true
            };

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.LoginUser(model.UserName, model.Password, model.RememberMe))
                .ReturnsAsync(false);

            var controller = new LoginController(mockUserRepository.Object);

            var result = await controller.Index(model) as ViewResult;

            Assert.NotNull(result);
            Assert.False(controller.ModelState.IsValid);
            Assert.Equal(model, result.Model);
        }
    }
}
