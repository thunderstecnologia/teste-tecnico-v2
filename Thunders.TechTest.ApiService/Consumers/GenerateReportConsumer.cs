using Newtonsoft.Json;
using System.Text;
using Thunders.TechTest.ApiService.Reports.Enums;
using Thunders.TechTest.ApiService.Reports.Strategies.Interfaces;
using Thunders.TechTest.ApiService.Reports.Strategies;
using Thunders.TechTest.ApiService.Reports;
using Thunders.TechTest.ApiService.Repositories.Configurations;
using Rebus.Handlers;

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
            var report = await strategy.GenerateReport(_dbContext, message);

            if (!string.IsNullOrWhiteSpace(message.CallbackUrl))
            {
                using var httpClient = new HttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(report), Encoding.UTF8, "application/json");
                await httpClient.PostAsync(message.CallbackUrl, content);
            }
            else
            {
                // Persista ou trate o relatório conforme necessário
            }
        }

        private object GenerateReportAsync(GenerateReportMessage message)
        {
            // Implemente a lógica de geração do relatório aqui
            return new { Message = "Report generated", GeneratedAt = DateTime.UtcNow };
        }
    }
}
