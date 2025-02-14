using Microsoft.AspNetCore.Mvc;
using Thunders.TechTest.ApiService.Dto.Request;
using Thunders.TechTest.ApiService.Dto.Response;

namespace Thunders.TechTest.ApiService.Controllers.Interfaces
{
    public interface ITollRecordController
    {
        Task<IActionResult> CreateAsync([FromBody] TollRecordRequestDto request);
    }
}
