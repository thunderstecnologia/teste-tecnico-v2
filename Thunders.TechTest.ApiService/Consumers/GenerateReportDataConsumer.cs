using Newtonsoft.Json;
using Rebus.Handlers;
using System.Text;
using Thunders.TechTest.ApiService.Reports;
using Thunders.TechTest.ApiService.Reports.Enums;
using Thunders.TechTest.ApiService.Reports.Strategies.Interfaces;
using Thunders.TechTest.ApiService.Repositories.Configurations;
using Thunders.TechTest.ApiService.Repositories.Interfaces;

namespace Thunders.TechTest.ApiService.Consumers
{
    public class GenerateReportDataConsumer : IHandleMessages<GenerateReportMessage>
    {
        private readonly AppDbContext _dbContext;
        private readonly IReportStrategyFactory _strategyFactory;
        private readonly IReportRepository _reportRepository;

        public GenerateReportDataConsumer(
            AppDbContext dbContext, 
            IReportStrategyFactory strategyFactory,
            IReportRepository reportRepository)
        {
            _dbContext = dbContext;
            _strategyFactory = strategyFactory;
            _reportRepository = reportRepository;
        }

        public async Task Handle(GenerateReportMessage message)
        {
            var report = await _reportRepository.GetByIdAsync(message.ReportId);
            if (report == null)
            {
                throw new InvalidOperationException($"Report with ID {message.ReportId} not found.");
            }

            report.ReportState = ReportState.InProgress;
            _dbContext.Reports.Update(report);
            await _dbContext.SaveChangesAsync();

            var strategy = _strategyFactory.GetStrategy(message.ReportType);
            var reportData = await strategy.GenerateReportData(_dbContext, message);

            var typedReportResult = (dynamic)reportData;

            var reportJson = JsonConvert.SerializeObject(reportData);

            report.GeneratedAt = DateTime.UtcNow;
            report.ReportState = ReportState.Completed;
            report.Data = reportJson;

            _dbContext.Reports.Update(report);
            await _dbContext.SaveChangesAsync();

            if (!string.IsNullOrWhiteSpace(message.CallbackUrl))
            {
                using var httpClient = new HttpClient();
                var content = new StringContent(reportJson, Encoding.UTF8, "application/json");
                await httpClient.PostAsync(message.CallbackUrl, content);
            }
        }
    }
}
