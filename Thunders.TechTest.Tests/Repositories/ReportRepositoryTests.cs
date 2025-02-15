using Microsoft.EntityFrameworkCore;
using Moq;
using Thunders.TechTest.ApiService.Models;
using Thunders.TechTest.ApiService.Reports.Enums;
using Thunders.TechTest.ApiService.Repositories;
using Thunders.TechTest.ApiService.Repositories.Configurations;

namespace Thunders.TechTest.Tests.Repositories
{
    public class ReportRepositoryTests
    {
        private readonly Mock<AppDbContext> _mockContext;
        private readonly ReportRepository _repository;

        public ReportRepositoryTests()
        {
            _mockContext = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
            _repository = new ReportRepository(_mockContext.Object);
        }

        [Fact]
        public async Task SaveReportRequestAsync_ShouldSaveReport()
        {
            // Arrange
            var report = new Report
            {
                Id = 1,
                ReportType = ReportType.TotalValueByHourByCity,
                ReportState = ReportState.Created,
                GeneratedAt = DateTime.UtcNow,
                CreatedBy = "testuser",
                CreatedAt = DateTime.UtcNow
            };

            _mockContext.Setup(m => m.Set<Report>().Add(report));
            _mockContext.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _repository.SaveReportRequestAsync(report);

            // Assert
            _mockContext.Verify(m => m.Set<Report>().Add(report), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
            Assert.Equal(report, result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnReport_WhenReportExists()
        {
            // Arrange
            var reportId = 1L;
            var report = new Report
            {
                Id = reportId,
                ReportType = ReportType.TotalValueByHourByCity,
                ReportState = ReportState.Created,
                GeneratedAt = DateTime.UtcNow,
                CreatedBy = "testuser",
                CreatedAt = DateTime.UtcNow
            };

            _mockContext.Setup(m => m.Reports.FindAsync(reportId)).ReturnsAsync(report);

            // Act
            var result = await _repository.GetByIdAsync(reportId);

            // Assert
            _mockContext.Verify(m => m.Reports.FindAsync(reportId), Times.Once);
            Assert.Equal(report, result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenReportDoesNotExist()
        {
            // Arrange
            var reportId = 1L;

            _mockContext.Setup(m => m.Reports.FindAsync(reportId)).ReturnsAsync((Report?)null);


            // Act
            var result = await _repository.GetByIdAsync(reportId);

            // Assert
            _mockContext.Verify(m => m.Reports.FindAsync(reportId), Times.Once);
            Assert.Null(result);
        }
    }
}
