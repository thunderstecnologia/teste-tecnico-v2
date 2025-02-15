using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Thunders.TechTest.ApiService.Dto.Filter;
using Thunders.TechTest.ApiService.Reports;
using Thunders.TechTest.ApiService.Reports.Enums;
using Thunders.TechTest.ApiService.Reports.Strategies.Interfaces;
using Thunders.TechTest.ApiService.Repositories.Configurations;

namespace Thunders.TechTest.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IEnumerable<IReportStrategy> _reportStrategies;

        public ReportController(AppDbContext dbContext, IEnumerable<IReportStrategy> reportStrategies)
        {
            _dbContext = dbContext;
            _reportStrategies = reportStrategies;
        }

        [HttpPost("total-value-by-hour-by-city")]
        public async Task<IActionResult> GetTotalValueByHourByCity([FromBody] DateRangeFilter dateRangeFilter)
        {
            var strategy = _reportStrategies.FirstOrDefault(s => s.ReportType == ReportType.TotalValueByHourByCity);
            if (strategy == null)
            {
                return NotFound("Report strategy not found.");
            }

            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            var report = await strategy.GenerateReport(_dbContext, new GenerateReportMessage { StartDate = dateRangeFilter.StartDate, EndDate = dateRangeFilter.EndDate, CreatedBy = email! });
            return Ok(report);
        }

        [HttpPost("top-toll-plazas-by-month")]
        public async Task<IActionResult> GetTopTollPlazasByMonth([FromBody] DateRangeFilter dateRangeFilter, [FromBody] QuantityFilter quantityFilter)
        {
            var strategy = _reportStrategies.FirstOrDefault(s => s.ReportType == ReportType.TopTollPlazas);
            if (strategy == null)
            {
                return NotFound("Report strategy not found.");
            }

            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            var report = await strategy.GenerateReport(_dbContext, new GenerateReportMessage { StartDate = dateRangeFilter.StartDate, EndDate = dateRangeFilter.EndDate, Quantity = quantityFilter.Quantity, CreatedBy = email! });
            return Ok(report);
        }

        [HttpPost("vehicle-types-by-toll-booth")]
        public async Task<IActionResult> GetVehicleTypesByTollBooth([FromBody] TollBoothFilter tollBoothFilter, [FromBody] DateRangeFilter dateRangeFilter)
        {
            var strategy = _reportStrategies.FirstOrDefault(s => s.ReportType == ReportType.VehicleTypeCount);
            if (strategy == null)
            {
                return NotFound("Report strategy not found.");
            }

            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            var report = await strategy.GenerateReport(_dbContext, new GenerateReportMessage { TollBooth = tollBoothFilter.TollBoothId, StartDate = dateRangeFilter.StartDate, EndDate = dateRangeFilter.EndDate, CreatedBy = email! });
            return Ok(report);
        }
    }
}
