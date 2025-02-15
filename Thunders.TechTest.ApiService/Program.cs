using Thunders.TechTest.ApiService.Configurations;
using Thunders.TechTest.ApiService.Configurations.Extensions;
using Thunders.TechTest.ApiService.Consumers;
using Thunders.TechTest.ApiService.Repositories.Seed;
using Thunders.TechTest.OutOfBox.Queues;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.Configure<AppConfiguration>(builder.Configuration);

builder.Host.ConfigureSerilog(builder.Configuration);

builder.Services.ConfigureIdentity(builder.Configuration);
builder.Services.ConfigureJwtAuthentication(builder.Configuration);
builder.Services.ConfigureOpenTelemetry(builder.Configuration);
builder.Services.ConfigureFeatures(builder.Configuration);
builder.Services.ConfigureSwagger(builder.Configuration);

builder.AddServiceDefaults();

builder.Services.RegisterInterfaces();
builder.Services.ConfigureMessageHandlers();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails();
builder.Services.AddHealthChecks();

builder.Services.AddBus(builder.Configuration, new SubscriptionBuilder()
            .Add<GenerateReportDataConsumer>());

var app = builder.Build();

app.ConfigureMiddlewares();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    await SeedData.InitializeAsync(services);
}

app.Run();
