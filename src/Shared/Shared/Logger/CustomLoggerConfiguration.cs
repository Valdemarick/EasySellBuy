using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace Shared.Logger;

public static class CustomLoggerConfiguration
{
    public static void ConfigureLogs(IConfigurationRoot configuration)
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .WriteTo.Debug()
            .WriteTo.Console()
            .WriteTo.Elasticsearch(ConfigureELS(configuration, env))
            .CreateLogger();
    }

    private static ElasticsearchSinkOptions ConfigureELS(IConfigurationRoot configuration, string? env)
    {
        return new ElasticsearchSinkOptions(new Uri(configuration["ELKConfiguration:Uri"]))
        {
            AutoRegisterTemplate = true,
            IndexFormat = $"{configuration["ELKConfiguration:ApiName"].ToLower()}-{env?.ToLower().Replace(".", ",")}-{DateTime.UtcNow:yyyy-MM}"
        };

    }
}