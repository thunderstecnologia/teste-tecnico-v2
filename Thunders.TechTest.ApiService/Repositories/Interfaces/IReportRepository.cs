using Thunders.TechTest.ApiService.Models;

namespace Thunders.TechTest.ApiService.Repositories.Interfaces
{
    public interface IReportRepository
    {
        Task<Report> SaveReportRequestAsync(Report report);
        Task<Report?> GetByIdAsync(long reportId);
    }
}
