namespace Thunders.TechTest.ApiService.Configurations.Options
{
    public class OpenTelemetryTracing
    {
        public bool AspNetCoreInstrumentation { get; set; }
        public bool HttpClientInstrumentation { get; set; }
        public required string Exporter { get; set; }
    }
}
