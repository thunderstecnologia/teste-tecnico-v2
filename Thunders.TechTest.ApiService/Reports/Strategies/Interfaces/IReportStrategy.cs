using Rebus.Bus;
using Thunders.TechTest.ApiService.Reports.Enums;
using Thunders.TechTest.ApiService.Repositories.Configurations;

namespace Thunders.TechTest.ApiService.Reports.Strategies.Interfaces
{
    public interface IReportStrategy
    {
        ReportType ReportType { get; }
        Task<object> GenerateReportData(AppDbContext dbContext, GenerateReportMessage message);
        Task SendMessageToRabbitMQ(IBus bus, GenerateReportMessage message);
    }
}
