using Thunders.TechTest.ApiService.Configurations.Options;

namespace Thunders.TechTest.ApiService.Configurations
{
    public class OpenTelemetrySettings
    {
        public required OpenTelemetryTracing Tracing { get; set; }
    }
}
