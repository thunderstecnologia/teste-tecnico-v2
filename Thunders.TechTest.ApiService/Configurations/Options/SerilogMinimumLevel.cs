namespace Thunders.TechTest.ApiService.Configurations.Options
{
    public class SerilogMinimumLevel
    {
        public required string Default { get; set; }
        public required Dictionary<string, string> Override { get; set; }
    }
}
