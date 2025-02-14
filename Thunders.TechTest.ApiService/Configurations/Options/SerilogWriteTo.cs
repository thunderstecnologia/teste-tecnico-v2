namespace Thunders.TechTest.ApiService.Configurations.Options
{
    public class SerilogWriteTo
    {
        public required string Name { get; set; }
        public required SerilogArgs Args { get; set; }
    }
}
