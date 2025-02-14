namespace Thunders.TechTest.ApiService.Configurations
{
    public class AppConfiguration
    {
        public required JwtSettings JwtSettings { get; set; }
        public required SwaggerSettings Swagger { get; set; }
        public required FeaturesSettings Features { get; set; }
        public required OpenTelemetrySettings OpenTelemetry { get; set; }
        public required SerilogSettings Serilog { get; set; }
    }
}
