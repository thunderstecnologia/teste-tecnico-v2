using Thunders.TechTest.ApiService.Repositories.Configurations;

namespace Thunders.TechTest.ApiService.Reports.Strategies.Interfaces
{
    public interface IReportStrategy
    {
        Task<object> GenerateReport(AppDbContext dbContext, GenerateReportMessage message);
    }
}
