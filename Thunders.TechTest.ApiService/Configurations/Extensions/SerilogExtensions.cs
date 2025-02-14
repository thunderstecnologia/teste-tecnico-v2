using Serilog;

namespace Thunders.TechTest.ApiService.Configurations.Extensions
{
    public static class SerilogExtensions
    {
        public static IHostBuilder ConfigureSerilog(this IHostBuilder hostBuilder, IConfiguration configuration)
        {
            var appConfig = configuration.Get<AppConfiguration>();
            if (appConfig == null || appConfig.Serilog == null)
                throw new InvalidOperationException("SerilogSettings not configured properly.");
            return hostBuilder.UseSerilog((context, loggerConfiguration) =>
            {
                loggerConfiguration.ReadFrom.Configuration(context.Configuration);
            });
        }
    }
}
