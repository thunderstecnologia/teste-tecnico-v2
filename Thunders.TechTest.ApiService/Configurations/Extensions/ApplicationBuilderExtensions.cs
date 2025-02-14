using Microsoft.Extensions.Options;
using Serilog;

namespace Thunders.TechTest.ApiService.Configurations.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static WebApplication ConfigureMiddlewares(this WebApplication app)
        {
            // Middleware de tratamento de exceções
            app.UseExceptionHandler();

            // Configuração do Swagger apenas para ambiente de desenvolvimento
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    // Usa IOptions para obter a configuração do Swagger
                    var appConfig = app.Services.GetRequiredService<IOptions<AppConfiguration>>().Value;
                    var swaggerSettings = appConfig.Swagger;

                    options.SwaggerEndpoint($"/swagger/{swaggerSettings.Version}/swagger.json", $"{swaggerSettings.Title} {swaggerSettings.Version}");
                    options.RoutePrefix = swaggerSettings.RoutePrefix;
                });
            }

            // Logging com Serilog
            app.UseSerilogRequestLogging();

            // Autenticação e Autorização
            app.UseAuthentication();
            app.UseAuthorization();

            // Mapeamento dos endpoints
            app.MapDefaultEndpoints();
            app.MapControllers();

            return app;
        }
    }
}