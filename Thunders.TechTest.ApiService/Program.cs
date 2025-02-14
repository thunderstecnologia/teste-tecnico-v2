using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Trace;
using Serilog;
using Thunders.TechTest.ApiService;
using Thunders.TechTest.ApiService.Configurations;
using Thunders.TechTest.ApiService.Configurations.Models;
using Thunders.TechTest.ApiService.Models;
using Thunders.TechTest.ApiService.Repositories.Configurations;
using Thunders.TechTest.OutOfBox.Database;
using Thunders.TechTest.OutOfBox.Queues;

var builder = WebApplication.CreateBuilder(args);

var environment = builder.Environment.EnvironmentName;

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureJwtAuthentication(builder.Configuration);

builder.Services.AddAuthorization();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddConsoleExporter());

builder.AddServiceDefaults();
builder.Services.AddControllers();

var features = Features.BindFromConfiguration(builder.Configuration);

builder.Services.AddProblemDetails();

if (features.UseMessageBroker)
{
    builder.Services.AddBus(builder.Configuration, new SubscriptionBuilder());
}

if (features.UseEntityFramework)
{
    builder.Services.AddSqlServerDbContext<DbContext>(builder.Configuration);
}

builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureSwagger(builder.Configuration);

builder.Services.AddHealthChecks();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var swaggerSettings = app.Configuration.GetSection("Swagger").Get<SwaggerSettings>();
        if (swaggerSettings != null)
        {
            options.SwaggerEndpoint($"/swagger/{swaggerSettings.Version}/swagger.json", $"{swaggerSettings.Title} {swaggerSettings.Version}");
            options.RoutePrefix = swaggerSettings.RoutePrefix;
        }
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler();
app.MapDefaultEndpoints();
app.UseSerilogRequestLogging();
app.MapControllers();

app.Run();
