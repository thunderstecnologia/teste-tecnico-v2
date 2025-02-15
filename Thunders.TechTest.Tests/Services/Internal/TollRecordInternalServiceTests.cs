using Moq;
using Thunders.TechTest.ApiService.Dto.Request;
using Thunders.TechTest.ApiService.Enums;
using Thunders.TechTest.ApiService.Models;
using Thunders.TechTest.ApiService.Repositories.Interfaces;
using Thunders.TechTest.ApiService.Services.Internal;

namespace Thunders.TechTest.Tests.Services.Internal
{
    public class TollRecordInternalServiceTests
    {
        private readonly Mock<ITollRecordRepository> _mockRepository;
        private readonly TollRecordInternalService _service;

        public TollRecordInternalServiceTests()
        {
            _mockRepository = new Mock<ITollRecordRepository>();
            _service = new TollRecordInternalService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllRecords()
        {
            // Arrange
            var records = new List<TollRecord>
            {
                new TollRecord { Id = 1, Timestamp = DateTime.Now, TollBooth = "Booth1", City = "City1", State = "State1", AmountPaid = 10, VehicleType = VehicleType.Car, LicensePlate = "ABC123", TransactionId = "TXN1", CreatedBy = "ADMIN" },
                new TollRecord { Id = 2, Timestamp = DateTime.Now, TollBooth = "Booth2", City = "City2", State = "State2", AmountPaid = 20, VehicleType = VehicleType.Truck, LicensePlate = "XYZ789", TransactionId = "TXN2", CreatedBy = "ADMIN" }
            };
            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(records);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnRecord_WhenRecordExists()
        {
            // Arrange
            var record = new TollRecord { Id = 1, Timestamp = DateTime.Now, TollBooth = "Booth1", City = "City1", State = "State1", AmountPaid = 10, VehicleType = VehicleType.Car, LicensePlate = "ABC123", TransactionId = "TXN1", CreatedBy = "ADMIN" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(record);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenRecordDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((TollRecord)null);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetByLicensePlateAsync_ShouldReturnRecords()
        {
            // Arrange
            var records = new List<TollRecord>
            {
                new TollRecord { Id = 1, Timestamp = DateTime.Now, TollBooth = "Booth1", City = "City1", State = "State1", AmountPaid = 10, VehicleType = VehicleType.Car, LicensePlate = "ABC123", TransactionId = "TXN1", CreatedBy = "ADMIN" }
            };
            _mockRepository.Setup(repo => repo.GetByLicensePlateAsync("ABC123")).ReturnsAsync(records);

            // Act
            var result = await _service.GetByLicensePlateAsync("ABC123");

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public async Task GetByTollBoothIdAsync_ShouldReturnRecords()
        {
            // Arrange
            var records = new List<TollRecord>
            {
                new TollRecord { Id = 1, Timestamp = DateTime.Now, TollBooth = "Booth1", City = "City1", State = "State1", AmountPaid = 10, VehicleType = VehicleType.Car, LicensePlate = "ABC123", TransactionId = "TXN1", CreatedBy = "ADMIN" }
            };
            _mockRepository.Setup(repo => repo.GetByTollBoothIdAsync("Booth1")).ReturnsAsync(records);

            // Act
            var result = await _service.GetByTollBoothIdAsync("Booth1");

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public async Task MarkAsDeletedAsync_ShouldCallRepository()
        {
            // Arrange
            var request = new MarkAsDeletedRequestDto { Id = 1, DeletedBy = "Admin" };

            // Act
            await _service.MarkAsDeletedAsync(request);

            // Assert
            _mockRepository.Verify(repo => repo.MarkAsDeletedAsync(1, "Admin"), Times.Once);
        }
    }
}
