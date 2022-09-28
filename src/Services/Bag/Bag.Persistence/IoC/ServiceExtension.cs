using Bag.Application.Cassandra.Interfaces;
using Bag.Application.Common.Interfaces.Repositories;
using Bag.Persistence.Contexts;
using Bag.Persistence.Repositories;
using Bag.Persistence.Repositories.Cassandra;
using Cassandra;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bag.Persistence.IoC;

public static class ServiceExtension
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .ConfigureDbContext(configuration)
            .AddDependencies(configuration)
            .ConfigureCassandraCluster(configuration);

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return services;
    }

    public async static Task<IApplicationBuilder> SeedDataAsync(this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        {
            var service = serviceScope.ServiceProvider.GetService<DataSeeder>();

            await service.InitializeDatabaseAsync();
        }

        return app;
    }

    private static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(config =>
        {
            config.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        return services;
    }

    private static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddScoped<IRepositoryManager, RepositoryManager>()
            .AddScoped<DataSeeder>()
            .AddScoped<ICassandraOrderRepository, CassandraOrderRepository>();

        return services;
    }

    private static IServiceCollection ConfigureCassandraCluster(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddScoped<ICluster, Cluster>(services => new Builder()
            .AddContactPoint(configuration["Cassandra:Host"])
            .WithPort(Int32.Parse(configuration["Cassandra:Port"]))
            .Build());

        return services;
    }
}