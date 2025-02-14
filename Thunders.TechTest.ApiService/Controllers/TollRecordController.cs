using Microsoft.AspNetCore.Mvc;
using Thunders.TechTest.ApiService.Controllers.Interfaces;
using Thunders.TechTest.ApiService.Dto.Request;
using Thunders.TechTest.ApiService.Dto.Response;
using Thunders.TechTest.ApiService.Services.Interfaces;

namespace Thunders.TechTest.ApiService.Controllers
{
    [ApiController]
    [Route("api/toll-records")]
    public class TollRecordController : ControllerBase, ITollRecordController
    {
        private readonly ITollRecordService _service;

        public TollRecordController(ITollRecordService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TollRecordRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _service.CreateAsync(request);
            return CreatedAtAction(nameof(CreateAsync), new { request.LicensePlate }, request);
        }
    }
}
