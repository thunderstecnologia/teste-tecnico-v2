using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Thunders.TechTest.ApiService.Controllers.Interfaces;
using Thunders.TechTest.ApiService.Dto.Request;
using Thunders.TechTest.ApiService.Repositories.Configurations;
using Thunders.TechTest.ApiService.Services.Interfaces;

namespace Thunders.TechTest.ApiService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase, IReportController
    {
        private readonly AppDbContext _dbContext;
        private readonly IReportService _reportService;

        public ReportController(
            AppDbContext dbContext,
            IReportService reportService)
        {
            _dbContext = dbContext;
            _reportService = reportService;
        }

        [HttpPost("GenerateTopTollPlazasReport")]
        public async Task<IActionResult> GenerateTopTollPlazasReportRequest([FromBody] TopTollPlazasReportRequestDto request)
        {
            var createdBy = User.FindFirstValue(ClaimTypes.Email) ?? User.Identity?.Name;
            if (createdBy == null)
            {
                return BadRequest("Unable to determine the user creating the report.");
            }

            request.CreatedBy = createdBy;
            var response = await _reportService.GenerateAndSaveReportAsync(request);
            return Ok(response);
        }

        [HttpPost("GenerateTotalValueByHourByCityReport")]
        public async Task<IActionResult> GenerateTotalValueByHourByCityReportRequest([FromBody] TotalValueByHourByCityReportRequestDto request)
        {
            var createdBy = User.FindFirstValue(ClaimTypes.Email) ?? User.Identity?.Name;
            if (createdBy == null)
            {
                return BadRequest("Unable to determine the user creating the report.");
            }

            request.CreatedBy = createdBy;
            var response = await _reportService.GenerateAndSaveReportAsync(request);
            return Ok(response);
        }

        [HttpPost("GenerateVehicleTypeCountReport")]
        public async Task<IActionResult> GenerateVehicleTypeCountReportRequest([FromBody] VehicleTypeCountReportRequestDto request)
        {
            var createdBy = User.FindFirstValue(ClaimTypes.Email) ?? User.Identity?.Name;
            if (createdBy == null)
            {
                return BadRequest("Unable to determine the user creating the report.");
            }

            request.CreatedBy = createdBy;
            var response = await _reportService.GenerateAndSaveReportAsync(request);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var report = await _reportService.GetReportByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }
    }
}
