using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.ApiService.Reports.Enums;
using Thunders.TechTest.ApiService.Reports.Strategies.Interfaces;
using Thunders.TechTest.ApiService.Repositories.Configurations;

namespace Thunders.TechTest.ApiService.Reports.Strategies
{
    public class VehicleTypeCountReportStrategy : IReportStrategy
    {
        public ReportType ReportType => ReportType.VehicleTypeCount;

        public async Task<object> GenerateReport(AppDbContext dbContext, GenerateReportMessage message)
        {
            var reportData = await dbContext.TollRecords
                .Where(r => r.TollBooth == message.TollBooth)
                .GroupBy(r => r.VehicleType)
                .Select(g => new
                {
                    VehicleType = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            return new
            {
                ReportType = ReportType.ToString(),
                GeneratedAt = DateTime.UtcNow,
                Data = reportData
            };
        }
    }
}
