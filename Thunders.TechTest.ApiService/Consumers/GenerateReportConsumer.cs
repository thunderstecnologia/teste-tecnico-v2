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

        public GenerateReportConsumer(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(GenerateReportMessage message)
        {
            // Processamento do relatório
            var report = GenerateReportAsync(message);

            if (!string.IsNullOrWhiteSpace(message.CallbackUrl))
            {
                using var httpClient = new HttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(report), Encoding.UTF8, "application/json");
                await httpClient.PostAsync(message.CallbackUrl, content);
            }
            else
            {
                // Persistir ou tratar o relatório conforme necessário
            }
        }

        private object GenerateReportAsync(GenerateReportMessage message)
        {
            // Implemente a lógica de geração do relatório aqui
            return new { Message = "Report generated", GeneratedAt = DateTime.UtcNow };
        }
    }
}
