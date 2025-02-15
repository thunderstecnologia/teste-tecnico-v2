using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Thunders.TechTest.ApiService.Reports.Enums;

namespace Thunders.TechTest.ApiService.Models
{
    [Table("Reports")]
    public class Report
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public required ReportType ReportType { get; set; }
        [Required]
        public required ReportState ReportState { get; set; }
        public DateTime GeneratedAt { get; set; }
        public string? Data { get; set; }
        [Required, MaxLength(100)]
        public required string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
