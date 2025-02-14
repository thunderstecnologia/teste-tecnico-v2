using System.ComponentModel.DataAnnotations;
using Thunders.TechTest.ApiService.Enums;

namespace Thunders.TechTest.ApiService.Dto.Request
{
    public class TollRecordRequestDto
    {
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
    }
}
