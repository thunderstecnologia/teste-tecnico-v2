using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.ApiService.Reports.Strategies.Interfaces;
using Thunders.TechTest.ApiService.Repositories.Configurations;

namespace Thunders.TechTest.ApiService.Reports.Strategies
{
    public class TotalValueByHourByCityReportStrategy : IReportStrategy
    {
        public async Task<object> GenerateReport(AppDbContext dbContext, GenerateReportMessage message)
        {
            var reportData = await dbContext.TollRecords
                .GroupBy(r => new { r.Timestamp.Hour, r.City })
                .Select(g => new
                {
                    g.Key.Hour,
                    g.Key.City,
                    TotalValue = g.Sum(r => r.AmountPaid)
                })
                .ToListAsync();

            return reportData;
        }
    }
}
