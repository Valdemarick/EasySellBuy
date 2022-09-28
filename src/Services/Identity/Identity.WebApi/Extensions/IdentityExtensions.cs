using Identity.WebApi.Data.Contexts;
using Identity.WebApi.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Identity.WebApi.Extensions;

public static class IdentityExtensions
{
    private static readonly string assemblyName = Assembly.GetExecutingAssembly().GetName().Name!;

    public static IServiceCollection AddCustomIdentity(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.ConfigureIdentityOptions()
            .ConfigureIdentityServer(configuration);

        return services;
    }

    private static IServiceCollection ConfigureIdentityOptions(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole<int>>(config =>
        {
            config.Password.RequiredLength = 8;
            config.Password.RequireDigit = true;
            config.Password.RequireNonAlphanumeric = false;
            config.Password.RequireUppercase = false;
        }).AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        return services;
    }

    private static IServiceCollection ConfigureIdentityServer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddIdentityServer()
            .AddTestUsers(IdentityConfig.GetTestUsers())
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(assemblyName));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(assemblyName));
            })
            .AddDeveloperSigningCredential();

        return services;
    }
}