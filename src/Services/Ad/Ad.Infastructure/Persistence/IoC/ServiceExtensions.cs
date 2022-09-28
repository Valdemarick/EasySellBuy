using Ad.Application.Interfaces.Repositories;
using Ad.Infrastructure.Persistence.Contexts;
using Ad.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ad.Infrastructure.Persistence.IoC;

public static class ServiceExtensions
{
    public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(config =>
        {
            config.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddScoped<IRepositoryManager, RepositoryManager>();

        return services;
    }
}