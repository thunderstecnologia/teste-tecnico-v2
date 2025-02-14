using Microsoft.AspNetCore.Mvc;
using Thunders.TechTest.ApiService.Dto.Request;
using Thunders.TechTest.ApiService.Dto.Response;

namespace Thunders.TechTest.ApiService.Controllers.Internal.Interfaces
{
    public interface ITollRecordInternalController
    {
        Task<ActionResult<IEnumerable<TollRecordResponseDto>>> GetAllAsync();
        Task<ActionResult<TollRecordResponseDto>> GetByIdAsync(long id);
        Task<ActionResult<IEnumerable<TollRecordResponseDto>>> GetByLicensePlateAsync(string licensePlate);
        Task<ActionResult<IEnumerable<TollRecordResponseDto>>> GetByTollBoothIdAsync(string tollBoothId);
        Task<IActionResult> MarkAsDeletedAsync([FromBody] MarkAsDeletedRequestDto request);
    }
}
