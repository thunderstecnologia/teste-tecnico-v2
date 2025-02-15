using Thunders.TechTest.ApiService.Dto.Request;

namespace Thunders.TechTest.ApiService.Services.Interfaces
{
    public interface ITollRecordService
    {
        Task<long> CreateAsync(TollRecordRequestDto request);
    }
}
