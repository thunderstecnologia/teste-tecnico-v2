using Thunders.TechTest.ApiService.Reports.Enums;

namespace Thunders.TechTest.ApiService.Dto
{
    public class ReportMetadataDto
    {
        public ReportType ReportType { get; set; }
        public DateTime GeneratedAt { get; set; }
        public object? Data { get; set; }
    }
}
