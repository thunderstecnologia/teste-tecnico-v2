using Thunders.TechTest.ApiService.Reports.Enums;
using Thunders.TechTest.ApiService.Reports.Strategies.Interfaces;

namespace Thunders.TechTest.ApiService.Reports.Strategies
{
    public class ReportStrategyFactory : IReportStrategyFactory
    {
        private readonly IDictionary<ReportType, IReportStrategy> _strategies;

        public ReportStrategyFactory(IEnumerable<IReportStrategy> strategies)
        {
            _strategies = strategies.ToDictionary(s => s.ReportType, s => s);
        }

        public IReportStrategy GetStrategy(ReportType reportType)
        {
            if (_strategies.TryGetValue(reportType, out var strategy))
            {
                return strategy;
            }
            throw new ArgumentException($"No strategy registered for {reportType}");
        }
    }
}
