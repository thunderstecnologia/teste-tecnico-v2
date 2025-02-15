using Thunders.TechTest.ApiService.Dto.Request;
using Thunders.TechTest.ApiService.Dto.Response;

namespace Thunders.TechTest.ApiService.Services.Interfaces
{
    public interface ITollRecordService
    {
        Task<long> CreateAsync(TollRecordRequestDto request);
    }
}
