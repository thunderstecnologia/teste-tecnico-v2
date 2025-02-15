using Thunders.TechTest.ApiService.Dto.Filter;

namespace Thunders.TechTest.ApiService.Dto.Request
{
    public class VehicleTypeCountReportRequestDto
    {
        public CallbackFilter? Callback { get; set; }
        public TollBoothFilter? TollBooth { get; set; }
    }
}
