using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thunders.TechTest.ApiService.Controllers.Interfaces;
using Thunders.TechTest.ApiService.Dto.Request;
using Thunders.TechTest.ApiService.Services.Interfaces;
using Thunders.TechTest.ApiService.Services.Internal.Interfaces;

namespace Thunders.TechTest.ApiService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/toll-records")]
    public class TollRecordController : ControllerBase, ITollRecordController
    {
        private readonly ITollRecordService _service;
        private readonly ITollRecordInternalService _internalService;
        private readonly ILogger<TollRecordController> _logger;

        public TollRecordController(
            ITollRecordService service,
            ITollRecordInternalService internalService,
            ILogger<TollRecordController> logger)
        {
            _service = service;
            _internalService = internalService;
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

            // O serviço agora retorna o ID do novo registro
            var createdRecordId = await _service.CreateAsync(request);

            // Usar o ITollRecordInternalService para buscar o recurso recém-criado
            var createdRecord = await _internalService.GetByIdAsync(createdRecordId);

            if (createdRecord == null)
            {
                return NotFound(); // Se o registro não foi encontrado
            }

            return Ok(createdRecord);
        }
    }
}
