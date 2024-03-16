using HouYun.Controllers;
using HouYun.IRepositories;
using HouYun.ViewModels.forUser;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HouYun.Tests
{
    public class RegistrationControllerTests
    {
        [Fact]
        public async Task Index_WithValidModelAndSuccessfulRegistration_RedirectsToVideoAction()
        {
            var model = new RegistrationViewModel
            {
                UserName = "testuser",
                Password = "testpassword"
            };

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.RegistrationUser(model))
                .ReturnsAsync(true);

            var controller = new RegistrationController(mockUserRepository.Object);

            var result = await controller.Index(model) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Video", result.ControllerName);
            Assert.Equal("", result.ActionName);
        }

        [Fact]
        public async Task Index_WithInvalidModel_ReturnsViewWithModelError()
        {
            var model = new RegistrationViewModel();

            var mockUserRepository = new Mock<IUserRepository>();

            var controller = new RegistrationController(mockUserRepository.Object);
            controller.ModelState.AddModelError("UserName", "Required");

            var result = await controller.Index(model) as ViewResult;

            Assert.NotNull(result);
            Assert.False(controller.ModelState.IsValid);
            Assert.Equal(model, result.Model);
        }

        [Fact]
        public async Task Index_WithInvalidCredentials_ReturnsViewWithModelError()
        {
            var model = new RegistrationViewModel
            {
                UserName = "testuser",
                Password = "testpassword"
            };

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.RegistrationUser(model))
                .ReturnsAsync(false);

            var controller = new RegistrationController(mockUserRepository.Object);

            var result = await controller.Index(model) as ViewResult;

            Assert.NotNull(result);
            Assert.False(controller.ModelState.IsValid);
            Assert.Equal(model, result.Model);
        }
    }
}
