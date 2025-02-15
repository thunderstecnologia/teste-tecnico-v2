using Microsoft.AspNetCore.Mvc;
using Thunders.TechTest.ApiService.Dto.Request;

namespace Thunders.TechTest.ApiService.Controllers.Interfaces
{
    public interface IReportController
    {
        Task<IActionResult> GenerateTotalValueByHourByCityReportRequest([FromBody] TotalValueByHourByCityReportRequestDto request);
        Task<IActionResult> GenerateTopTollPlazasReportRequest([FromBody] TopTollPlazasReportRequestDto request);
        Task<IActionResult> GenerateVehicleTypeCountReportRequest([FromBody] VehicleTypeCountReportRequestDto request);
        Task<IActionResult> GetByIdAsync(long id);
    }
}
