using Thunders.TechTest.ApiService.Reports.Enums;

namespace Thunders.TechTest.ApiService.Reports
{
    public class GenerateReportMessage
    {
        public long ReportId { get; set; }
        public required ReportType ReportType { get; set; }
        public string? CallbackUrl { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Quantity { get; set; }
        public string? TollBooth { get; set; }
        public required string CreatedBy { get; set; }
    }
}
