using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using Thunders.TechTest.ApiService.Controllers;
using Thunders.TechTest.ApiService.Dto.Request;
using Thunders.TechTest.ApiService.Dto.Response;
using Thunders.TechTest.ApiService.Services.Interfaces;

namespace Thunders.TechTest.Tests.Controllers
{
    public class ReportControllerTests
    {
        private readonly Mock<IReportService> _mockReportService;
        private readonly ReportController _controller;

        public ReportControllerTests()
        {
            _mockReportService = new Mock<IReportService>();
            _controller = new ReportController(null, _mockReportService.Object);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, "test@example.com")
            }, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        [Fact]
        public async Task GenerateTopTollPlazasReportRequest_ReturnsOkResult()
        {
            // Arrange
            var request = new TopTollPlazasReportRequestDto { CreatedBy = "test@example.com" };
            var response = new ReportRequestResponseDto { Id = 1 };
            _mockReportService.Setup(s => s.GenerateAndSaveReportAsync(request)).ReturnsAsync(response);

            // Act
            var result = await _controller.GenerateTopTollPlazasReportRequest(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(response, okResult.Value);
        }

        [Fact]
        public async Task GenerateTotalValueByHourByCityReportRequest_ReturnsOkResult()
        {
            // Arrange
            var request = new TotalValueByHourByCityReportRequestDto { CreatedBy = "test@example.com" };
            var response = new ReportRequestResponseDto { Id = 1 };
            _mockReportService.Setup(s => s.GenerateAndSaveReportAsync(request)).ReturnsAsync(response);

            // Act
            var result = await _controller.GenerateTotalValueByHourByCityReportRequest(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(response, okResult.Value);
        }

        [Fact]
        public async Task GenerateVehicleTypeCountReportRequest_ReturnsOkResult()
        {
            // Arrange
            var request = new VehicleTypeCountReportRequestDto { CreatedBy = "test@example.com" };
            var response = new ReportRequestResponseDto { Id = 1 };
            _mockReportService.Setup(s => s.GenerateAndSaveReportAsync(request)).ReturnsAsync(response);

            // Act
            var result = await _controller.GenerateVehicleTypeCountReportRequest(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(response, okResult.Value);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsOkResult_WhenReportExists()
        {
            // Arrange
            var reportId = 1L;
            var reportData = "Report Data";
            _mockReportService.Setup(s => s.GetReportByIdAsync(reportId)).ReturnsAsync(reportData);

            // Act
            var result = await _controller.GetByIdAsync(reportId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(reportData, okResult.Value);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNotFound_WhenReportDoesNotExist()
        {
            // Arrange
            var reportId = 1L;
            _mockReportService.Setup(s => s.GetReportByIdAsync(reportId)).ReturnsAsync((string)null);

            // Act
            var result = await _controller.GetByIdAsync(reportId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
