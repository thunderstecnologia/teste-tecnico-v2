using Thunders.TechTest.ApiService.Dto.Request;
using Thunders.TechTest.ApiService.Dto.Response;
using Thunders.TechTest.ApiService.Models;
using Thunders.TechTest.ApiService.Reports;

namespace Thunders.TechTest.ApiService.Mappers.Interfaces
{
    public interface IReportMapper
    {
        Report MapToCreateReportRequest(TotalValueByHourByCityReportRequestDto request);
        Report MapToCreateReportRequest(TopTollPlazasReportRequestDto request);
        Report MapToCreateReportRequest(VehicleTypeCountReportRequestDto request);
        ReportRequestResponseDto MapToReportRequestResponse(Report savedReport);
        GenerateReportMessage MapToGenerateReportMessage(TotalValueByHourByCityReportRequestDto request);
        GenerateReportMessage MapToGenerateReportMessage(TopTollPlazasReportRequestDto request);
        GenerateReportMessage MapToGenerateReportMessage(VehicleTypeCountReportRequestDto request);
    }
}
