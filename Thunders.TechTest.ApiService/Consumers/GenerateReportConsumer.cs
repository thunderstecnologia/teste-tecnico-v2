using Newtonsoft.Json;
using Rebus.Handlers;
using System.Text;
using Thunders.TechTest.ApiService.Models;
using Thunders.TechTest.ApiService.Reports;
using Thunders.TechTest.ApiService.Reports.Strategies.Interfaces;
using Thunders.TechTest.ApiService.Repositories.Configurations;

namespace Thunders.TechTest.ApiService.Consumers
{
    public class GenerateReportConsumer : IHandleMessages<GenerateReportMessage>
    {
        private readonly AppDbContext _dbContext;
        private readonly IReportStrategyFactory _strategyFactory;

        public GenerateReportConsumer(AppDbContext dbContext, IReportStrategyFactory strategyFactory)
        {
            _dbContext = dbContext;
            _strategyFactory = strategyFactory;
        }

        public async Task Handle(GenerateReportMessage message)
        {
            var strategy = _strategyFactory.GetStrategy(message.ReportType);
            var reportResult = await strategy.GenerateReport(_dbContext, message);

            var typedReportResult = (dynamic)reportResult;

            var reportJson = JsonConvert.SerializeObject(reportResult);

            var reportEntity = new Report
            {
                ReportType = typedReportResult.ReportType.ToString(),
                GeneratedAt = DateTime.UtcNow,
                Data = reportJson,
                CreatedBy = typedReportResult.CreatedBy,
                CreatedAt = typedReportResult.CreatedAt
            };

            _dbContext.Reports.Add(reportEntity);
            await _dbContext.SaveChangesAsync();

            if (!string.IsNullOrWhiteSpace(message.CallbackUrl))
            {
                using var httpClient = new HttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(reportResult), Encoding.UTF8, "application/json");
                await httpClient.PostAsync(message.CallbackUrl, content);
            }
            else
            {
                var filePath = Path.Combine("Reports", $"{Guid.NewGuid()}.json");
                await File.WriteAllTextAsync(filePath, reportJson);
            }
        }
    }
}
