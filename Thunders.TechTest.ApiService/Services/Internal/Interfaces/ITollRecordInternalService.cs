using Thunders.TechTest.ApiService.Dto.Request;
using Thunders.TechTest.ApiService.Dto.Response;

namespace Thunders.TechTest.ApiService.Services.Internal.Interfaces
{
    public interface ITollRecordInternalService
    {
        Task<IEnumerable<TollRecordResponseDto>> GetAllAsync();
        Task<TollRecordResponseDto?> GetByIdAsync(long id);
        Task<IEnumerable<TollRecordResponseDto?>> GetByLicensePlateAsync(string licensePlate);
        Task<IEnumerable<TollRecordResponseDto?>> GetByTollBoothIdAsync(string tollBoothId);
        Task MarkAsDeletedAsync(MarkAsDeletedRequestDto request);
    }
}
