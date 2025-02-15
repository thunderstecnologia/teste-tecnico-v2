using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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

        [HttpGet("total-value-by-hour-by-city")]
        public async Task<IActionResult> GetTotalValueByHourByCity([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var strategy = _reportStrategies.FirstOrDefault(s => s.ReportType == ReportType.TotalValueByHourByCity);
            if (strategy == null)
            {
                return NotFound("Report strategy not found.");
            }

            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            var report = await strategy.GenerateReport(_dbContext, new GenerateReportMessage { StartDate = startDate, EndDate = endDate, CreatedBy = email! });
            return Ok(report);
        }

        [HttpGet("top-toll-plazas-by-month")]
        public async Task<IActionResult> GetTopTollPlazasByMonth([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, [FromQuery] int? quantity)
        {
            var strategy = _reportStrategies.FirstOrDefault(s => s.ReportType == ReportType.TopTollPlazas);
            if (strategy == null)
            {
                return NotFound("Report strategy not found.");
            }

            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            var report = await strategy.GenerateReport(_dbContext, new GenerateReportMessage { StartDate = startDate, EndDate = endDate, Quantity = quantity, CreatedBy = email! });
            return Ok(report);
        }

        [HttpGet("vehicle-types-by-toll-booth")]
        public async Task<IActionResult> GetVehicleTypesByTollBooth([FromQuery] string tollBooth, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var strategy = _reportStrategies.FirstOrDefault(s => s.ReportType == ReportType.VehicleTypeCount);
            if (strategy == null)
            {
                return NotFound("Report strategy not found.");
            }

            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            var report = await strategy.GenerateReport(_dbContext, new GenerateReportMessage { TollBooth = tollBooth, StartDate = startDate, EndDate = endDate, CreatedBy = email! });
            return Ok(report);
        }
    }
}
