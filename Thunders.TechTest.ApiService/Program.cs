using Thunders.TechTest.ApiService.Configurations;
using Thunders.TechTest.ApiService.Configurations.Extensions;
using Thunders.TechTest.ApiService.Repositories.Seed;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.Configure<AppConfiguration>(builder.Configuration);

builder.Host.ConfigureSerilog(builder.Configuration);

builder.Services
    .ConfigureDatabase(builder.Configuration)
    .ConfigureIdentity(builder.Configuration)
    .ConfigureJwtAuthentication(builder.Configuration)
    .ConfigureOpenTelemetry(builder.Configuration)
    .ConfigureFeatures(builder.Configuration)
    .ConfigureSwagger(builder.Configuration)
    .ConfigureRabbitMQ(builder.Configuration)
    .ConfigureCors();

builder.AddServiceDefaults();

builder.Services
    .ConfigureMappers()
    .ConfigureStrategies()
    .ConfigureRepositories()
    .ConfigureServices()
    .ConfigureControllers()
    .AddControllers();

builder.Services
    .AddEndpointsApiExplorer()
    .AddProblemDetails()
    .AddHealthChecks();

var app = builder.Build();

app.ConfigureMiddlewares();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    await SeedData.InitializeAsync(services);
}

app.Run();
