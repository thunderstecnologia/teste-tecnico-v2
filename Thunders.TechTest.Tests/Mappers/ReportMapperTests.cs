using Thunders.TechTest.ApiService.Dto.Filter;
using Thunders.TechTest.ApiService.Dto.Request;
using Thunders.TechTest.ApiService.Mappers;
using Thunders.TechTest.ApiService.Models;
using Thunders.TechTest.ApiService.Reports.Enums;

namespace Thunders.TechTest.Tests.Mappers
{
    public class ReportMapperTests
    {
        private readonly ReportMapper _mapper;

        public ReportMapperTests()
        {
            _mapper = new ReportMapper();
        }

        [Fact]
        public void MapToCreateReportRequest_TotalValueByHourByCityReportRequestDto_ReturnsReport()
        {
            // Arrange
            var request = new TotalValueByHourByCityReportRequestDto
            {
                CreatedBy = "testuser"
            };

            // Act
            var result = _mapper.MapToCreateReportRequest(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(request.ReportType, result.ReportType);
            Assert.Equal(ReportState.Created, result.ReportState);
            Assert.Equal(request.CreatedBy, result.CreatedBy);
            Assert.True((DateTime.UtcNow - result.CreatedAt).TotalSeconds < 1);
        }

        [Fact]
        public void MapToCreateReportRequest_TopTollPlazasReportRequestDto_ReturnsReport()
        {
            // Arrange
            var request = new TopTollPlazasReportRequestDto
            {
                CreatedBy = "testuser"
            };

            // Act
            var result = _mapper.MapToCreateReportRequest(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(request.ReportType, result.ReportType);
            Assert.Equal(ReportState.Created, result.ReportState);
            Assert.Equal(request.CreatedBy, result.CreatedBy);
            Assert.True((DateTime.UtcNow - result.CreatedAt).TotalSeconds < 1);
        }

        [Fact]
        public void MapToCreateReportRequest_VehicleTypeCountReportRequestDto_ReturnsReport()
        {
            // Arrange
            var request = new VehicleTypeCountReportRequestDto
            {
                CreatedBy = "testuser"
            };

            // Act
            var result = _mapper.MapToCreateReportRequest(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(request.ReportType, result.ReportType);
            Assert.Equal(ReportState.Created, result.ReportState);
            Assert.Equal(request.CreatedBy, result.CreatedBy);
            Assert.True((DateTime.UtcNow - result.CreatedAt).TotalSeconds < 1);
        }

        [Fact]
        public void MapToReportRequestResponse_ReturnsReportRequestResponseDto()
        {
            // Arrange
            var report = new Report
            {
                Id = 1,
                ReportType = ReportType.TotalValueByHourByCity,
                ReportState = ReportState.Created,
                CreatedBy = "testuser",
                CreatedAt = DateTime.UtcNow
            };

            // Act
            var result = _mapper.MapToReportRequestResponse(report);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(report.Id, result.Id);
        }

        [Fact]
        public void MapToGenerateReportMessage_TotalValueByHourByCityReportRequestDto_ReturnsGenerateReportMessage()
        {
            // Arrange
            var request = new TotalValueByHourByCityReportRequestDto
            {
                CreatedBy = "testuser",
                CallbackFilter = new CallbackFilter { CallbackUrl = "http://callback.url" },
                DateRangeFilter = new DateRangeFilter { StartDate = DateTime.UtcNow.AddDays(-1), EndDate = DateTime.UtcNow }
            };

            // Act
            var result = _mapper.MapToGenerateReportMessage(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(request.ReportType, result.ReportType);
            Assert.Equal(request.CreatedBy, result.CreatedBy);
            Assert.Equal(request.CallbackFilter.CallbackUrl, result.CallbackUrl);
            Assert.Equal(request.DateRangeFilter.StartDate, result.StartDate);
            Assert.Equal(request.DateRangeFilter.EndDate, result.EndDate);
        }

        [Fact]
        public void MapToGenerateReportMessage_TopTollPlazasReportRequestDto_ReturnsGenerateReportMessage()
        {
            // Arrange
            var request = new TopTollPlazasReportRequestDto
            {
                CreatedBy = "testuser",
                Callback = new CallbackFilter { CallbackUrl = "http://callback.url" },
                DateRangeFilter = new DateRangeFilter { StartDate = DateTime.UtcNow.AddDays(-1), EndDate = DateTime.UtcNow },
                QuantityFilter = new QuantityFilter { Quantity = 10 }
            };

            // Act
            var result = _mapper.MapToGenerateReportMessage(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(request.ReportType, result.ReportType);
            Assert.Equal(request.CreatedBy, result.CreatedBy);
            Assert.Equal(request.Callback.CallbackUrl, result.CallbackUrl);
            Assert.Equal(request.DateRangeFilter.StartDate, result.StartDate);
            Assert.Equal(request.DateRangeFilter.EndDate, result.EndDate);
            Assert.Equal(request.QuantityFilter.Quantity, result.Quantity);
        }

        [Fact]
        public void MapToGenerateReportMessage_VehicleTypeCountReportRequestDto_ReturnsGenerateReportMessage()
        {
            // Arrange
            var request = new VehicleTypeCountReportRequestDto
            {
                CreatedBy = "testuser",
                CallbackFilter = new CallbackFilter { CallbackUrl = "http://callback.url" },
                TollBoothFilter = new TollBoothFilter { TollBooth = "TollBooth1" }
            };

            // Act
            var result = _mapper.MapToGenerateReportMessage(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(request.ReportType, result.ReportType);
            Assert.Equal(request.CreatedBy, result.CreatedBy);
            Assert.Equal(request.CallbackFilter.CallbackUrl, result.CallbackUrl);
            Assert.Equal(request.TollBoothFilter.TollBooth, result.TollBooth);
        }
    }
}
