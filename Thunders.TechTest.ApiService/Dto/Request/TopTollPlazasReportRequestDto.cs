using Thunders.TechTest.ApiService.Dto.Filter;
using Thunders.TechTest.ApiService.Reports.Enums;

namespace Thunders.TechTest.ApiService.Dto.Request
{
    public class TopTollPlazasReportRequestDto
    {
        public required string CreatedBy { get; set; }
        public CallbackFilter? Callback { get; set; }
        public DateRangeFilter? DateRangeFilter { get; set; }
        public QuantityFilter? QuantityFilter { get; set; }
        public ReportType ReportType { get; } = ReportType.TopTollPlazas;
    }
}
