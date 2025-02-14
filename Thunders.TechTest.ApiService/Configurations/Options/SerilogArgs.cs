namespace Thunders.TechTest.ApiService.Configurations.Options
{
    public class SerilogArgs
    {
        public required string Path { get; set; }
        public required string RollingInterval { get; set; }
    }
}
