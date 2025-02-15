using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.ApiService.Reports.Strategies.Interfaces;
using Thunders.TechTest.ApiService.Repositories.Configurations;

namespace Thunders.TechTest.ApiService.Reports.Strategies
{
    public class VehicleTypeCountReportStrategy : IReportStrategy
    {
        public async Task<object> GenerateReport(AppDbContext dbContext, GenerateReportMessage message)
        {
            var reportData = await dbContext.TollRecords
                .Where(r => r.TollBooth == message.TollBoothId)
                .GroupBy(r => r.VehicleType)
                .Select(g => new
                {
                    VehicleType = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            return reportData;
        }
    }
}
