using Thunders.TechTest.ApiService.Models;

namespace Thunders.TechTest.ApiService.Repositories.Interfaces
{
    public interface ITollRecordRepository : IRepository<TollRecord, long>
    {
        Task<IEnumerable<TollRecord?>> GetByTollBoothIdAsync(string tollBoothId);
        Task<IEnumerable<TollRecord?>> GetByLicensePlateAsync(string licensePlate);
        Task MarkAsDeletedAsync(long id, string deletedBy);
    }
}
