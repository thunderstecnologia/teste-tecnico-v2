using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.ApiService.Dto;
using Thunders.TechTest.ApiService.Reports.Enums;
using Thunders.TechTest.ApiService.Reports.Strategies.Interfaces;
using Thunders.TechTest.ApiService.Repositories.Configurations;

namespace Thunders.TechTest.ApiService.Reports.Strategies
{
    public class TopTollPlazasReportStrategy : IReportStrategy
    {
        public ReportType ReportType => ReportType.TopTollPlazas;

        public async Task<ReportMetadataDto> GenerateReportData(AppDbContext dbContext, GenerateReportMessage message)
        {
            var reportData = await dbContext.TollRecords
                .Where(r =>
                    (!message.StartDate.HasValue || r.Timestamp >= message.StartDate.Value) &&
                    (!message.EndDate.HasValue || r.Timestamp <= message.EndDate.Value))
                .GroupBy(r => r.TollBooth)
                .Select(g => new
                {
                    Plaza = g.Key,
                    TotalValue = g.Sum(r => r.AmountPaid)
                })
                .OrderByDescending(x => x.TotalValue)
                .Take(message.Quantity ?? 5)
                .ToListAsync();

            return new ReportMetadataDto
            {
                ReportType = ReportType,
                GeneratedAt = DateTime.UtcNow,
                Data = reportData
            };
        }
    }
}
