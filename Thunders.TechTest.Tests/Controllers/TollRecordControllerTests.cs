using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Thunders.TechTest.ApiService.Controllers;
using Thunders.TechTest.ApiService.Dto.Request;
using Thunders.TechTest.ApiService.Dto.Response;
using Thunders.TechTest.ApiService.Enums;
using Thunders.TechTest.ApiService.Services.Interfaces;
using Thunders.TechTest.ApiService.Services.Internal.Interfaces;
using Xunit;

namespace Thunders.TechTest.Tests.Controllers
{
    public class TollRecordControllerTests
    {
        private readonly Mock<ITollRecordService> _mockTollRecordService;
        private readonly Mock<ITollRecordInternalService> _mockTollRecordInternalService;
        private readonly Mock<ILogger<TollRecordController>> _mockLogger;
        private readonly TollRecordController _controller;

        public TollRecordControllerTests()
        {
            _mockTollRecordService = new Mock<ITollRecordService>();
            _mockTollRecordInternalService = new Mock<ITollRecordInternalService>();
            _mockLogger = new Mock<ILogger<TollRecordController>>();
            _controller = new TollRecordController(
                _mockTollRecordService.Object,
                _mockTollRecordInternalService.Object,
                _mockLogger.Object);
        }

        [Fact]
        public async Task CreateAsync_InvalidModelState_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "some error");
            var request = new TollRecordRequestDto
            {
                TollBooth = "TollBooth1",
                City = "City1",
                State = "State1",
                LicensePlate = "ABC123",
                CreatedBy = "test@example.com"
            };

            // Act
            var result = await _controller.CreateAsync(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task CreateAsync_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var request = new TollRecordRequestDto
            {
                Timestamp = DateTime.UtcNow,
                TollBooth = "TollBooth1",
                City = "City1",
                State = "State1",
                AmountPaid = 10.0m,
                VehicleType = VehicleType.Car,
                LicensePlate = "ABC123",
                CreatedBy = "test@example.com"
            };
            var createdRecordId = 1L;
            var createdRecord = new TollRecordResponseDto
            {
                Id = createdRecordId,
                Timestamp = request.Timestamp,
                TollBooth = request.TollBooth,
                City = request.City,
                State = request.State,
                AmountPaid = request.AmountPaid,
                VehicleType = request.VehicleType,
                LicensePlate = request.LicensePlate
            };

            _mockTollRecordService.Setup(s => s.CreateAsync(request)).ReturnsAsync(createdRecordId);
            _mockTollRecordInternalService.Setup(s => s.GetByIdAsync(createdRecordId)).ReturnsAsync(createdRecord);

            // Act
            var result = await _controller.CreateAsync(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(createdRecord, okResult.Value);
        }

        [Fact]
        public async Task CreateAsync_RecordNotFound_ReturnsNotFound()
        {
            // Arrange
            var request = new TollRecordRequestDto
            {
                Timestamp = DateTime.UtcNow,
                TollBooth = "TollBooth1",
                City = "City1",
                State = "State1",
                AmountPaid = 10.0m,
                VehicleType = VehicleType.Car,
                LicensePlate = "ABC123",
                CreatedBy = "test@example.com"
            };
            var createdRecordId = 1L;

            _mockTollRecordService.Setup(s => s.CreateAsync(request)).ReturnsAsync(createdRecordId);
            _mockTollRecordInternalService.Setup(s => s.GetByIdAsync(createdRecordId)).ReturnsAsync((TollRecordResponseDto)null);

            // Act
            var result = await _controller.CreateAsync(request);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
