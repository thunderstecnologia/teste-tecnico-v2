using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thunders.TechTest.ApiService.Models
{
    [Table("Reports")]
    public class Report
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public required string ReportType { get; set; }
        public DateTime GeneratedAt { get; set; }
        public string Data { get; set; }
        [Required, MaxLength(100)]
        public required string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
