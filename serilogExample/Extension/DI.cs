using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;
using Serilog.Sinks.Elasticsearch;
using serilogExample.Enrichers;
using serilogExample.Services;

namespace serilogExample.Extension
{
    public static class DI
    {
        public static IServiceCollection AddDI(this IServiceCollection services)
        {
            services.AddScoped<ITesteService, TesteService>();
            return services;
        }

        public static Action<ILoggingBuilder> SetConfiguration(IConfiguration _configuration)
        {
            return loggingBuilder =>
            {
                //saca de appsettings en key value creado por nosotros
                var nodeUri = _configuration.GetValue<string>("LoggingOptions:NodeUri");

                var loggerConfiguration = new LoggerConfiguration()
                    .MinimumLevel.Information()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                    .WriteTo.Console(
                        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Properties}{NewLine}{Exception}"
                    )
                    .WriteTo.File("log.txt")
                    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(nodeUri))
                    {
                        AutoRegisterTemplate = true,
                        IndexFormat = $"Logs-{DateTime.Now:yyyy-MM}"
                    })
                    .Enrich.WithMachineName()
                    .Enrich.WithAssemblyName()
                    .Enrich.WithProperty("CustomProperty", "Latino .NET Online")
                    //custom para crear nuestro propio enricher
                    .Enrich.With<CustomEnricher>();
                    ;

                var logger = loggerConfiguration.CreateLogger();

                loggingBuilder.Services.AddSingleton<ILoggerFactory>(
                        provider => new SerilogLoggerFactory(logger, dispose: false));
            };
        }
    }
}