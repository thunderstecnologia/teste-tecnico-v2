using Microsoft.AspNetCore.Mvc;
using Thunders.TechTest.ApiService.Controllers.Internal.Interfaces;
using Thunders.TechTest.ApiService.Dto.Request;
using Thunders.TechTest.ApiService.Dto.Response;

namespace Thunders.TechTest.ApiService.Controllers.Internal
{
    [ApiController]
    [Route("api/internal/toll-records")]
    public class TollRecordInternalController : ControllerBase, ITollRecordInternalController
    {
        private readonly ITollRecordInternalController _service;

        public TollRecordInternalController(ITollRecordInternalController service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TollRecordResponseDto>>> GetAllAsync()
        {
            var records = await _service.GetAllAsync();
            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TollRecordResponseDto>> GetByIdAsync(long id)
        {
            var record = await _service.GetByIdAsync(id);
            if (record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }

        [HttpGet("license/{licensePlate}")]
        public async Task<ActionResult<IEnumerable<TollRecordResponseDto>>> GetByLicensePlateAsync(string licensePlate)
        {
            var records = await _service.GetByLicensePlateAsync(licensePlate);
            return Ok(records);
        }

        [HttpGet("toll-booth/{tollBoothId}")]
        public async Task<ActionResult<IEnumerable<TollRecordResponseDto>>> GetByTollBoothIdAsync(string tollBoothId)
        {
            var records = await _service.GetByTollBoothIdAsync(tollBoothId);
            return Ok(records);
        }

        [HttpPatch("mark-as-deleted")]
        public async Task<IActionResult> MarkAsDeletedAsync([FromBody] MarkAsDeletedRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _service.MarkAsDeletedAsync(request);
            return NoContent();
        }
    }
}
