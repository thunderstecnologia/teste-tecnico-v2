using Thunders.TechTest.ApiService.Dto.Filter;
using Thunders.TechTest.ApiService.Reports.Enums;

namespace Thunders.TechTest.ApiService.Dto.Request
{
    public class VehicleTypeCountReportRequestDto
    {
        public required string CreatedBy { get; set; }
        public CallbackFilter? CallbackFilter { get; set; }
        public TollBoothFilter? TollBoothFilter { get; set; }
        public ReportType ReportType { get; } = ReportType.VehicleTypeCount;
    }
}
