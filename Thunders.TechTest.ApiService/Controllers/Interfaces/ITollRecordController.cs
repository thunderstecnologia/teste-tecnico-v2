using Microsoft.AspNetCore.Mvc;
using Thunders.TechTest.ApiService.Dto.Request;

namespace Thunders.TechTest.ApiService.Controllers.Interfaces
{
    public interface ITollRecordController
    {
        Task<IActionResult> CreateAsync([FromBody] TollRecordRequestDto request);
    }
}
