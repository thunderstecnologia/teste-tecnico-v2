using Thunders.TechTest.ApiService.Enums;

namespace Thunders.TechTest.ApiService.Dto.Response
{
    public class TollRecordResponseDto
    {
        public long Id { get; set; }
        public DateTime Timestamp { get; set; }
        public required string TollBooth { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public decimal AmountPaid { get; set; }
        public VehicleType VehicleType { get; set; }
        public required string LicensePlate { get; set; }
        public string? TransactionId { get; set; }
    }
}
