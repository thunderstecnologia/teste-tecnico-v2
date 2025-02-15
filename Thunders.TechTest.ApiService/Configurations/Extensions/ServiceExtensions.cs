using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Trace;
using Rebus.Config;
using System.Text;
using Thunders.TechTest.ApiService.Consumers;
using Thunders.TechTest.ApiService.Controllers;
using Thunders.TechTest.ApiService.Controllers.Interfaces;
using Thunders.TechTest.ApiService.Controllers.Internal;
using Thunders.TechTest.ApiService.Controllers.Internal.Interfaces;
using Thunders.TechTest.ApiService.Mapper;
using Thunders.TechTest.ApiService.Mapper.Interfaces;
using Thunders.TechTest.ApiService.Reports.Strategies;
using Thunders.TechTest.ApiService.Reports.Strategies.Interfaces;
using Thunders.TechTest.ApiService.Repositories;
using Thunders.TechTest.ApiService.Repositories.Configurations;
using Thunders.TechTest.ApiService.Repositories.Interfaces;
using Thunders.TechTest.ApiService.Services;
using Thunders.TechTest.ApiService.Services.Interfaces;
using Thunders.TechTest.ApiService.Services.Internal;
using Thunders.TechTest.ApiService.Services.Internal.Interfaces;
using Thunders.TechTest.OutOfBox.Database;
using Thunders.TechTest.OutOfBox.Queues;

namespace Thunders.TechTest.ApiService.Configurations.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }

        public static IServiceCollection ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            return services;
        }

        public static IServiceCollection ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var appConfig = configuration.Get<AppConfiguration>();
            if (appConfig == null || appConfig.JwtSettings == null)
                throw new InvalidOperationException("AppConfiguration or JwtSettings not configured properly.");
            var jwtSettings = appConfig.JwtSettings;
            if (string.IsNullOrEmpty(jwtSettings.Secret))
                throw new InvalidOperationException("JWT Secret is not configured properly.");
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = jwtSettings.RequireHttpsMetadata;
                options.SaveToken = jwtSettings.SaveToken;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = jwtSettings.ValidateIssuer,
                    ValidateAudience = jwtSettings.ValidateAudience,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    ValidateLifetime = jwtSettings.ValidateLifetime
                };
            });
            return services;
        }

        public static IServiceCollection ConfigureOpenTelemetry(this IServiceCollection services, IConfiguration configuration)
        {
            var appConfig = configuration.Get<AppConfiguration>();
            services.AddOpenTelemetry().WithTracing(builder =>
            {
                if (appConfig?.OpenTelemetry?.Tracing != null)
                {
                    var tracing = appConfig.OpenTelemetry.Tracing;
                    if (tracing.AspNetCoreInstrumentation)
                        builder.AddAspNetCoreInstrumentation();
                    if (tracing.HttpClientInstrumentation)
                        builder.AddHttpClientInstrumentation();
                    if (!string.IsNullOrEmpty(tracing.Exporter) && tracing.Exporter.Equals("Console", StringComparison.OrdinalIgnoreCase))
                        builder.AddConsoleExporter();
                }
                else
                {
                    builder.AddAspNetCoreInstrumentation()
                           .AddHttpClientInstrumentation()
                           .AddConsoleExporter();
                }
            });
            return services;
        }

        public static IServiceCollection ConfigureFeatures(this IServiceCollection services, IConfiguration configuration)
        {
            var appConfig = configuration.Get<AppConfiguration>();
            if (appConfig == null || appConfig.Features == null)
                throw new InvalidOperationException("AppConfiguration or FeaturesSettings not configured properly.");
            var features = appConfig.Features;
            if (features.UseMessageBroker)
            {
                services.AddBus(configuration, new SubscriptionBuilder());
            }
            if (features.UseEntityFramework)
            {
                services.AddSqlServerDbContext<DbContext>(configuration);
            }
            return services;
        }

        public static IServiceCollection ConfigureSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            var appConfig = configuration.Get<AppConfiguration>();
            if (appConfig == null || appConfig.Swagger == null)
                throw new InvalidOperationException("AppConfiguration or SwaggerSettings not configured properly.");

            var swaggerSettings = appConfig.Swagger;
            services.Configure<SwaggerSettings>(configuration.GetSection("Swagger"));
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(swaggerSettings.Version, new OpenApiInfo
                {
                    Title = swaggerSettings.Title,
                    Version = swaggerSettings.Version,
                    Description = swaggerSettings.Description
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Insira o token JWT no formato: Bearer {seu_token}"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
            return services;
        }

        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });
            return services;
        }

        public static IServiceCollection ConfigureRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqSettings = configuration.GetSection("RabbitMQ").Get<RabbitMQSettings>();
            if (rabbitMqSettings == null)
                throw new InvalidOperationException("RabbitMQSettings not configured properly.");

            services.Configure<RabbitMQSettings>(configuration.GetSection("RabbitMQ"));

            services.AddRebus(configure => configure
                .Transport(t => t.UseRabbitMq(rabbitMqSettings.Host, rabbitMqSettings.Queue))
            );

            services.AutoRegisterHandlersFromAssemblyOf<GenerateReportDataConsumer>();

            return services;
        }

        public static IServiceCollection ConfigureControllers(this IServiceCollection services)
        {
            services.AddScoped<ITollRecordController, TollRecordController>();
            services.AddScoped<ITollRecordInternalController, TollRecordInternalController>();
            services.AddScoped<IReportController, ReportController>();
            return services;
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<ITollRecordService, TollRecordService>();
            services.AddScoped<ITollRecordInternalService, TollRecordInternalService>();
            services.AddScoped<IReportService, ReportService>();
            return services;
        }

        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITollRecordRepository, TollRecordRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            return services;
        }

        public static IServiceCollection ConfigureStrategies(this IServiceCollection services)
        {
            services.AddTransient<IReportStrategy, TotalValueByHourByCityReportStrategy>();
            services.AddTransient<IReportStrategy, TopTollPlazasReportStrategy>();
            services.AddTransient<IReportStrategy, VehicleTypeCountReportStrategy>();

            services.AddSingleton<IReportStrategyFactory, ReportStrategyFactory>();

            return services;
        }

        public static IServiceCollection ConfigureMappers(this IServiceCollection services)
        {
            services.AddScoped<IReportMapper, ReportMapper>();

            return services;
        }
    }
}
