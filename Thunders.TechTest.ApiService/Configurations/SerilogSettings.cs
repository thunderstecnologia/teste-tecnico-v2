using Thunders.TechTest.ApiService.Configurations.Options;

namespace Thunders.TechTest.ApiService.Configurations
{
    public class SerilogSettings
    {
        public required string[] Using { get; set; }
        public required SerilogMinimumLevel MinimumLevel { get; set; }
        public required SerilogWriteTo[] WriteTo { get; set; }
        public required string[] Enrich { get; set; }
    }
}
