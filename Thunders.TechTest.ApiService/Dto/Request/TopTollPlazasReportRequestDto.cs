using Thunders.TechTest.ApiService.Dto.Filter;

namespace Thunders.TechTest.ApiService.Dto.Request
{
    public class TopTollPlazasReportRequestDto
    {
        public CallbackFilter? Callback { get; set; }
        public DateRangeFilter? DateRange { get; set; }
        public QuantityFilter? Quantity { get; set; }
    }
}
