using Bag.Application.Cassandra.Mapping;
using Bag.Application.Common.Interfaces.Services;
using Bag.Application.Common.Redis;
using Bag.Application.Services;
using Cassandra.Mapping;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Bag.Application.IoC;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddFluentValidation(config =>
            {
                config.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            })
            .AddServices()
            .AddRedis(configuration)
            .ConfigureRedisOptions(configuration);

        MappingConfiguration.Global.Define<CassandraMappings>();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ISellerService, SellerService>();

        services.AddScoped<ICustomerService, CustomerService>();

        services.AddScoped<IOrderService, OrderService>();

        services.AddScoped<IDistributedCacheService, DistributedCacheService>();

        return services;
    }

    private static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = $"{configuration["RedisCache:Host"]}:{configuration["RedisCache:Port"]}";
            options.InstanceName = configuration["RedisCache:InstanceName"];
        });

        return services;
    }

    private static IServiceCollection ConfigureRedisOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RedisOptions>(options => configuration.GetSection("Redis").Bind(options));

        return services;
    }
}