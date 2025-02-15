using Thunders.TechTest.ApiService.Dto.Request;
using Thunders.TechTest.ApiService.Dto.Response;

namespace Thunders.TechTest.ApiService.Services.Interfaces
{
    public interface IReportService
    {
        Task<ReportRequestResponseDto> GenerateAndSaveReportAsync(TotalValueByHourByCityReportRequestDto request);
        Task<ReportRequestResponseDto> GenerateAndSaveReportAsync(TopTollPlazasReportRequestDto request);
        Task<ReportRequestResponseDto> GenerateAndSaveReportAsync(VehicleTypeCountReportRequestDto request);
        Task<string> GetReportByIdAsync(long reportId);
    }
}
