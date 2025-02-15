using Microsoft.EntityFrameworkCore;
using Moq;
using Thunders.TechTest.ApiService.Enums;
using Thunders.TechTest.ApiService.Models;
using Thunders.TechTest.ApiService.Repositories;
using Thunders.TechTest.ApiService.Repositories.Configurations;

namespace Thunders.TechTest.Tests.Repositories
{
    public class TollRecordRepositoryTests
    {
        private readonly AppDbContext _context;
        private readonly TollRecordRepository _repository;

        public TollRecordRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new AppDbContext(options);
            _repository = new TollRecordRepository(_context);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddEntity()
        {
            // Arrange
            var tollRecord = new TollRecord { Id = 2, Timestamp = DateTime.Now, TollBooth = "Booth1", City = "City1", State = "State1", AmountPaid = 10, VehicleType = VehicleType.Car, LicensePlate = "ABC123", TransactionId = "TXN1", CreatedBy = "ADMIN" };

            // Act
            var result = await _repository.CreateAsync(tollRecord);

            // Assert
            var addedRecord = await _context.TollRecords.FindAsync(result);
            Assert.NotNull(addedRecord);
            Assert.Equal(2, result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllRecords()
        {
            // Arrange
            var data = new List<TollRecord>
                        {
                            new TollRecord { Id = 3, Timestamp = DateTime.Now, TollBooth = "Booth1", City = "City1", State = "State1", AmountPaid = 10, VehicleType = VehicleType.Car, LicensePlate = "ABC123", TransactionId = "TXN1", CreatedBy = "ADMIN" },
                            new TollRecord { Id = 4, Timestamp = DateTime.Now, TollBooth = "Booth1", City = "City1", State = "State1", AmountPaid = 10, VehicleType = VehicleType.Car, LicensePlate = "EFG456", TransactionId = "TXN2", CreatedBy = "ADMIN" }
                        };

            await _context.TollRecords.AddRangeAsync(data);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnRecord()
        {
            // Arrange
            var tollRecord = new TollRecord { Id =5, Timestamp = DateTime.Now, TollBooth = "Booth1", City = "City1", State = "State1", AmountPaid = 10, VehicleType = VehicleType.Car, LicensePlate = "ABC123", TransactionId = "TXN1", CreatedBy = "ADMIN" };
            await _context.TollRecords.AddAsync(tollRecord);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetByIdAsync(5);

            // Assert
            Assert.Equal(tollRecord, result);
        }

        [Fact]
        public async Task GetByLicensePlateAsync_ShouldReturnRecords()
        {
            // Arrange
            var data = new List<TollRecord>
                        {
                            new TollRecord { Id = 6, Timestamp = DateTime.Now, TollBooth = "Booth1", City = "City1", State = "State1", AmountPaid = 10, VehicleType = VehicleType.Car, LicensePlate = "ABC999", TransactionId = "TXN1", CreatedBy = "ADMIN" },
                            new TollRecord { Id = 7, Timestamp = DateTime.Now, TollBooth = "Booth1", City = "City1", State = "State1", AmountPaid = 10, VehicleType = VehicleType.Car, LicensePlate = "EFG456", TransactionId = "TXN2", CreatedBy = "ADMIN" }
                        };

            await _context.TollRecords.AddRangeAsync(data);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetByLicensePlateAsync("ABC999");

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public async Task GetByTollBoothIdAsync_ShouldReturnRecords()
        {
            // Arrange
            var data = new List<TollRecord>
                        {
                            new TollRecord { Id = 8, Timestamp = DateTime.Now, TollBooth = "Booth999", City = "City1", State = "State1", AmountPaid = 10, VehicleType = VehicleType.Car, LicensePlate = "ABC123", TransactionId = "TXN1", CreatedBy = "ADMIN" },
                            new TollRecord { Id = 9, Timestamp = DateTime.Now, TollBooth = "Booth999", City = "City1", State = "State1", AmountPaid = 10, VehicleType = VehicleType.Car, LicensePlate = "EFG456", TransactionId = "TXN2", CreatedBy = "ADMIN" }
                        };

            await _context.TollRecords.AddRangeAsync(data);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetByTollBoothIdAsync("Booth999");

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task MarkAsDeletedAsync_ShouldMarkRecordAsDeleted()
        {
            // Arrange
            var tollRecord = new TollRecord { Id = 10, Timestamp = DateTime.Now, TollBooth = "Booth777", City = "City1", State = "State1", AmountPaid = 10, VehicleType = VehicleType.Car, LicensePlate = "ABC123", TransactionId = "TXN1", CreatedBy = "ADMIN" };
            await _context.TollRecords.AddAsync(tollRecord);
            await _context.SaveChangesAsync();

            // Act
            await _repository.MarkAsDeletedAsync(10, "admin");

            // Assert
            var deletedRecord = await _context.TollRecords.FindAsync((long)10);
            Assert.NotNull(deletedRecord);
            Assert.True(deletedRecord.IsDeleted);
            Assert.Equal("admin", deletedRecord.DeletedBy);
        }
    }
}
