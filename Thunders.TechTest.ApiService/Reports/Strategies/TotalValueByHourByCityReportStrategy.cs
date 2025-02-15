using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.ApiService.Dto;
using Thunders.TechTest.ApiService.Reports.Enums;
using Thunders.TechTest.ApiService.Reports.Strategies.Interfaces;
using Thunders.TechTest.ApiService.Repositories.Configurations;

namespace Thunders.TechTest.ApiService.Reports.Strategies
{
    public class TotalValueByHourByCityReportStrategy : IReportStrategy
    {
        public ReportType ReportType => ReportType.TotalValueByHourByCity;

        public async Task<ReportMetadataDto> GenerateReportData(AppDbContext dbContext, GenerateReportMessage message)
        {
            var reportData = await dbContext.TollRecords
                .Where(r => 
                    (!message.StartDate.HasValue || r.Timestamp >= message.StartDate.Value) && 
                    (!message.EndDate.HasValue || r.Timestamp <= message.EndDate.Value))
                .GroupBy(r => new 
                { 
                    Hour = r.Timestamp.Hour, 
                    r.City 
                })
                .Select(g => new 
                { 
                    Hour = g.Key.Hour, 
                    City = g.Key.City, 
                    TotalValue = g.Sum(r => r.AmountPaid) 
                })
                .OrderBy(x => x.Hour)
                .ThenBy(x => x.City)
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
