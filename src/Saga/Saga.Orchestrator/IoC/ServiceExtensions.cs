using Saga.Orchestrator.Models.Clients;
using Saga.Orchestrator.Services;

namespace Saga.Orchestrator.IoC;

public static class ServiceExtensions
{
    public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient(configuration["AdClient:Name"], c => c.BaseAddress = new Uri(configuration["AdClient:Url"]));
        services.AddHttpClient(configuration["BagClient:Name"], c => c.BaseAddress = new Uri(configuration["BagClient:Url"]));

        return services;
    }

    public static IServiceCollection AddClientOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AdClientOptions>(options => configuration.GetSection("AdClient").Bind(options));

        services.Configure<BagClientOptions>(options => configuration.GetSection("BagClient").Bind(options));

        return services;
    }

    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<IOrchestrator, CustomOrchestrator>();

        return services;
    }
}