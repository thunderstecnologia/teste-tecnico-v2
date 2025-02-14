using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Trace;
using System.Text;
using Thunders.TechTest.ApiService.Models;
using Thunders.TechTest.ApiService.Repositories.Configurations;
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
            services.AddIdentity<User, IdentityRole>()
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

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });
        }

    }
}
