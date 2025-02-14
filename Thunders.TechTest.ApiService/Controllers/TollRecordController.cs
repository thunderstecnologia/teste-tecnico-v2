using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thunders.TechTest.ApiService.Controllers.Interfaces;
using Thunders.TechTest.ApiService.Dto.Request;
using Thunders.TechTest.ApiService.Dto.Response;
using Thunders.TechTest.ApiService.Services.Interfaces;

namespace Thunders.TechTest.ApiService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/toll-records")]
    public class TollRecordController : ControllerBase, ITollRecordController
    {
        private readonly ITollRecordService _service;
        private readonly ILogger<TollRecordController> _logger;

        public TollRecordController(
            ITollRecordService service,
            ILogger<TollRecordController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TollRecordRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid request: {@request}", request);
                return BadRequest(ModelState);
            }
            await _service.CreateAsync(request);
            return CreatedAtAction(nameof(CreateAsync), new { request.LicensePlate }, request);
        }
    }
}
