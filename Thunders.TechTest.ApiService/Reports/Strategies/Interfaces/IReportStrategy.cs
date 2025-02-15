using Thunders.TechTest.ApiService.Dto;
using Thunders.TechTest.ApiService.Reports.Enums;
using Thunders.TechTest.ApiService.Repositories.Configurations;

namespace Thunders.TechTest.ApiService.Reports.Strategies.Interfaces
{
    public interface IReportStrategy
    {
        ReportType ReportType { get; }
        Task<ReportMetadataDto> GenerateReportData(AppDbContext dbContext, GenerateReportMessage message);
    }
}
