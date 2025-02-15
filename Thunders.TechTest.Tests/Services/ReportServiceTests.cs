using Moq;
using Rebus.Bus;
using System.Text.Json;
using Thunders.TechTest.ApiService.Dto.Request;
using Thunders.TechTest.ApiService.Dto.Response;
using Thunders.TechTest.ApiService.Mappers.Interfaces;
using Thunders.TechTest.ApiService.Models;
using Thunders.TechTest.ApiService.Reports;
using Thunders.TechTest.ApiService.Reports.Enums;
using Thunders.TechTest.ApiService.Repositories.Interfaces;
using Thunders.TechTest.ApiService.Services;

namespace Thunders.TechTest.Tests.Services
{
    public class ReportServiceTests
    {
        private readonly Mock<IReportRepository> _mockReportRepository;
        private readonly Mock<IReportMapper> _mockReportMapper;
        private readonly Mock<IBus> _mockBus;
        private readonly ReportService _reportService;

        public ReportServiceTests()
        {
            _mockReportRepository = new Mock<IReportRepository>();
            _mockReportMapper = new Mock<IReportMapper>();
            _mockBus = new Mock<IBus>();
            _reportService = new ReportService(_mockBus.Object, _mockReportRepository.Object, _mockReportMapper.Object);
        }

        [Fact]
        public async Task GenerateAndSaveReportAsync_TotalValueByHourByCityReportRequestDto_Success()
        {
            // Arrange
            var request = new TotalValueByHourByCityReportRequestDto { CreatedBy = "testuser" };
            var report = new Report
            {
                Id = 1,
                CreatedBy = "testuser",
                ReportType = ReportType.TotalValueByHourByCity,
                ReportState = ReportState.Created
            };
            var message = new GenerateReportMessage
            {
                CreatedBy = "testuser",
                ReportType = ReportType.TotalValueByHourByCity // Set required ReportType
            };

            _mockReportMapper.Setup(m => m.MapToCreateReportRequest(request)).Returns(report);
            _mockReportMapper.Setup(m => m.MapToGenerateReportMessage(request)).Returns(message);
            _mockReportRepository.Setup(r => r.SaveReportRequestAsync(report)).ReturnsAsync(report);
            _mockReportMapper.Setup(m => m.MapToReportRequestResponse(report)).Returns(new ReportRequestResponseDto() { Id = report.Id });

            // Act
            var result = await _reportService.GenerateAndSaveReportAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(report.Id, result.Id);
        }

        [Fact]
        public async Task GenerateAndSaveReportAsync_TopTollPlazasReportRequestDto_Success()
        {
            // Arrange
            var request = new TopTollPlazasReportRequestDto { CreatedBy = "testuser" };
            var report = new Report
            {
                Id = 1,
                CreatedBy = "testuser",
                ReportType = ReportType.TopTollPlazas, // Set required ReportType
                ReportState = ReportState.Created // Set required ReportState
            };
            var message = new GenerateReportMessage
            {
                CreatedBy = "testuser",
                ReportType = ReportType.TopTollPlazas // Set required ReportType
            };

            _mockReportMapper.Setup(m => m.MapToCreateReportRequest(request)).Returns(report);
            _mockReportMapper.Setup(m => m.MapToGenerateReportMessage(request)).Returns(message);
            _mockReportRepository.Setup(r => r.SaveReportRequestAsync(report)).ReturnsAsync(report);
            _mockReportMapper.Setup(m => m.MapToReportRequestResponse(report)).Returns(new ReportRequestResponseDto() {  Id = report.Id });

            // Act
            var result = await _reportService.GenerateAndSaveReportAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(report.Id, result.Id);
        }

        [Fact]
        public async Task GenerateAndSaveReportAsync_VehicleTypeCountReportRequestDto_Success()
        {
            // Arrange
            var request = new VehicleTypeCountReportRequestDto { CreatedBy = "testuser" };
            var report = new Report
            {
                Id = 1,
                CreatedBy = "testuser",
                ReportType = ReportType.VehicleTypeCount, // Set required ReportType
                ReportState = ReportState.Created // Set required ReportState
            };
            var message = new GenerateReportMessage
            {
                CreatedBy = "testuser",
                ReportType = ReportType.VehicleTypeCount // Set required ReportType
            };

            _mockReportMapper.Setup(m => m.MapToCreateReportRequest(request)).Returns(report);
            _mockReportMapper.Setup(m => m.MapToGenerateReportMessage(request)).Returns(message);
            _mockReportRepository.Setup(r => r.SaveReportRequestAsync(report)).ReturnsAsync(report);
            _mockReportMapper.Setup(m => m.MapToReportRequestResponse(report)).Returns(new ReportRequestResponseDto() { Id = report.Id });

            // Act
            var result = await _reportService.GenerateAndSaveReportAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(report.Id, result.Id);
        }

        [Fact]
        public async Task GetReportByIdAsync_ReportExists_ReturnsReportData()
        {
            // Arrange
            var reportId = 1;
            var report = new Report
            {
                Id = 1,
                CreatedBy = "testuser",
                ReportType = ReportType.VehicleTypeCount, // Set required ReportType
                ReportState = ReportState.Created // Set required ReportState
            };

            _mockReportRepository.Setup(r => r.GetByIdAsync(reportId)).ReturnsAsync(report);

            // Act
            var result = await _reportService.GetReportByIdAsync(reportId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(JsonSerializer.Serialize(report.Data), result);
        }

        [Fact]
        public async Task GetReportByIdAsync_ReportDoesNotExist_ThrowsKeyNotFoundException()
        {
            // Arrange
            var reportId = 1;

            _mockReportRepository.Setup(r => r.GetByIdAsync(reportId)).ReturnsAsync((Report)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _reportService.GetReportByIdAsync(reportId));
        }
    }
}
