using Thunders.TechTest.ApiService.Reports.Enums;

namespace Thunders.TechTest.ApiService.Reports.Strategies.Interfaces
{
    public interface IReportStrategyFactory
    {
        IReportStrategy GetStrategy(ReportType reportType);
    }
}
