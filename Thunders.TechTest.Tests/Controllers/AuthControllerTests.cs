using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Thunders.TechTest.ApiService.Controllers;
using Thunders.TechTest.ApiService.Repositories.Seed;

namespace Thunders.TechTest.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<UserManager<IdentityUser>> _mockUserManager;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _mockUserManager = new Mock<UserManager<IdentityUser>>(
                Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
            _mockConfiguration = new Mock<IConfiguration>();
            _controller = new AuthController(_mockUserManager.Object, _mockConfiguration.Object);
        }

        [Fact]
        public async Task Login_Success()
        {
            // Arrange
            var user = new IdentityUser { Email = "test@example.com", UserName = "testuser" };
            var loginRequest = new LoginRequest { Email = "admin@thunders.com", Password = "Admin@123" };

            _mockUserManager.Setup(um => um.FindByEmailAsync(loginRequest.Email)).ReturnsAsync(user);
            _mockUserManager.Setup(um => um.CheckPasswordAsync(user, loginRequest.Password)).ReturnsAsync(true);
            _mockConfiguration.Setup(c => c.GetSection("JwtSettings")["Secret"]).Returns("SuperStrongSecretKeyWithAtLeast32Characters");

            // Act
            var result = await _controller.Login(loginRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task Login_InvalidCredentials()
        {
            // Arrange
            var loginRequest = new LoginRequest { Email = "test@example.com", Password = "WrongPassword" };

            _mockUserManager.Setup(um => um.FindByEmailAsync(loginRequest.Email)).ReturnsAsync((IdentityUser)null);

            // Act
            var result = await _controller.Login(loginRequest);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Invalid email or password.", unauthorizedResult.Value);
        }
    }
}
