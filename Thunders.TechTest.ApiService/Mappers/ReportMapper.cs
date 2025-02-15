using Thunders.TechTest.ApiService.Dto.Request;
using Thunders.TechTest.ApiService.Dto.Response;
using Thunders.TechTest.ApiService.Mappers.Interfaces;
using Thunders.TechTest.ApiService.Models;
using Thunders.TechTest.ApiService.Reports;
using Thunders.TechTest.ApiService.Reports.Enums;

namespace Thunders.TechTest.ApiService.Mappers
{
    public class ReportMapper : IReportMapper
    {
        public Report MapToCreateReportRequest(TotalValueByHourByCityReportRequestDto request)
        {
            return new Report
            {
                ReportType = request.ReportType,
                ReportState = ReportState.Created,
                CreatedBy = request.CreatedBy,
                CreatedAt = DateTime.UtcNow,
            };
        }

        public Report MapToCreateReportRequest(TopTollPlazasReportRequestDto request)
        {
            return new Report
            {
                ReportType = request.ReportType,
                ReportState = ReportState.Created,
                CreatedBy = request.CreatedBy,
                CreatedAt = DateTime.UtcNow,
            };
        }

        public Report MapToCreateReportRequest(VehicleTypeCountReportRequestDto request)
        {
            return new Report
            {
                ReportType = request.ReportType,
                ReportState = ReportState.Created,
                CreatedBy = request.CreatedBy,
                CreatedAt = DateTime.UtcNow,
            };
        }

        public ReportRequestResponseDto MapToReportRequestResponse(Report savedReport)
        {
            return new ReportRequestResponseDto
            {
                Id = savedReport.Id
            };
        }

        public GenerateReportMessage MapToGenerateReportMessage(TotalValueByHourByCityReportRequestDto request)
        {
            return new GenerateReportMessage
            {
                ReportType = request.ReportType,
                CreatedBy = request.CreatedBy,
                CallbackUrl = request.CallbackFilter?.CallbackUrl,
                StartDate = request.DateRangeFilter?.StartDate,
                EndDate = request.DateRangeFilter?.EndDate
            };
        }

        public GenerateReportMessage MapToGenerateReportMessage(TopTollPlazasReportRequestDto request)
        {
            return new GenerateReportMessage
            {
                ReportType = request.ReportType,
                CreatedBy = request.CreatedBy,
                CallbackUrl = request.Callback?.CallbackUrl,
                StartDate = request.DateRangeFilter?.StartDate,
                EndDate = request.DateRangeFilter?.EndDate,
                Quantity = request.QuantityFilter?.Quantity
            };
        }

        public GenerateReportMessage MapToGenerateReportMessage(VehicleTypeCountReportRequestDto request)
        {
            return new GenerateReportMessage
            {
                ReportType = request.ReportType,
                CreatedBy = request.CreatedBy,
                CallbackUrl = request.CallbackFilter?.CallbackUrl,
                TollBooth = request.TollBoothFilter?.TollBooth
            };
        }
    }
}
