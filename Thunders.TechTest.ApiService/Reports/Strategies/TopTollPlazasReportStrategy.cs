using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.ApiService.Reports.Strategies.Interfaces;
using Thunders.TechTest.ApiService.Repositories.Configurations;

namespace Thunders.TechTest.ApiService.Reports.Strategies
{
    public class TopTollPlazasReportStrategy : IReportStrategy
    {
        public async Task<object> GenerateReport(AppDbContext dbContext, GenerateReportMessage message)
        {
            var reportData = await dbContext.TollRecords
                .Where(r => r.Timestamp >= message.StartDate && r.Timestamp <= message.EndDate)
                .GroupBy(r => r.TollBooth)
                .Select(g => new
                {
                    Plaza = g.Key,
                    TotalValue = g.Sum(r => r.AmountPaid)
                })
                .OrderByDescending(x => x.TotalValue)
                .Take(message.Quantity ?? 5)
                .ToListAsync();

            return reportData;
        }
    }
}
