using Moq;
using Thunders.TechTest.ApiService.Dto.Request;
using Thunders.TechTest.ApiService.Enums;
using Thunders.TechTest.ApiService.Models;
using Thunders.TechTest.ApiService.Repositories.Interfaces;
using Thunders.TechTest.ApiService.Services;

namespace Thunders.TechTest.Tests.Services
{
    public class TollRecordServiceTests
    {
        private readonly Mock<ITollRecordRepository> _mockRepository;
        private readonly TollRecordService _service;

        public TollRecordServiceTests()
        {
            _mockRepository = new Mock<ITollRecordRepository>();
            _service = new TollRecordService(_mockRepository.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnNewRecordId()
        {
            // Arrange
            var request = new TollRecordRequestDto
            {
                Timestamp = DateTime.UtcNow,
                TollBooth = "Booth1",
                City = "City1",
                State = "State1",
                AmountPaid = 10.0m,
                VehicleType = VehicleType.Car,
                LicensePlate = "ABC123",
                TransactionId = "TX123",
                CreatedBy = "User1"
            };

            var newRecordId = 1L;
            _mockRepository.Setup(repo => repo.CreateAsync(It.IsAny<TollRecord>())).ReturnsAsync(newRecordId);

            // Act
            var result = await _service.CreateAsync(request);

            // Assert
            Assert.Equal(newRecordId, result);
            _mockRepository.Verify(repo => repo.CreateAsync(It.Is<TollRecord>(r =>
                r.Timestamp == request.Timestamp &&
                r.TollBooth == request.TollBooth &&
                r.City == request.City &&
                r.State == request.State &&
                r.AmountPaid == request.AmountPaid &&
                r.VehicleType == request.VehicleType &&
                r.LicensePlate == request.LicensePlate &&
                r.TransactionId == request.TransactionId &&
                r.CreatedBy == request.CreatedBy &&
                r.CreatedAt <= DateTime.UtcNow
            )), Times.Once);
        }
    }
}
