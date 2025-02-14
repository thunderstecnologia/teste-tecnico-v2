using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Thunders.TechTest.ApiService.Enums;

namespace Thunders.TechTest.ApiService.Models
{
    [Table("TollRecords")]
    public class TollRecord
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Required, MaxLength(100)]
        public required string TollBooth { get; set; }

        [Required, MaxLength(100)]
        public required string City { get; set; }

        [Required, MaxLength(100)]
        public required string State { get; set; }

        [Required]
        public decimal AmountPaid { get; set; }

        [Required]
        public VehicleType VehicleType { get; set; }

        [Required, MaxLength(100)]
        public required string LicensePlate { get; set; }

        [MaxLength(100)]
        public string? TransactionId { get; set; }

        [Required, MaxLength(100)]
        public required string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public bool IsDeleted { get; set; } = false;

        public long? PreviousRecordId { get; set; }

        [MaxLength(100)]
        public string? DeletedBy { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
