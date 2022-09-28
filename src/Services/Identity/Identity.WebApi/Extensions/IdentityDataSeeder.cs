using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Identity.WebApi.Extensions;

public static class IdentityDataSeeder
{
    public static async Task<IApplicationBuilder> SeedDataAsync(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();

        serviceScope?.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

        var context = serviceScope?.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

        context!.Database.Migrate();

        await SeedAsync(context!, new CancellationTokenSource().Token);

        return app;
    }

    private async static Task SeedAsync(ConfigurationDbContext context, CancellationToken cancellationToken = default)
    {
        await SeedClientsAsync(context, cancellationToken);
        await SeedIdentityResources(context, cancellationToken);
        await SeedApiScopes(context, cancellationToken);
        await SeedApiResources(context, cancellationToken);
    }

    private async static Task SeedClientsAsync(ConfigurationDbContext context, CancellationToken cancellationToken = default)
    {
        if (!await context.Clients.AnyAsync(cancellationToken))
        {
            foreach (var client in IdentityConfig.GetClients())
            {
                await context.Clients.AddAsync(client.ToEntity(), cancellationToken);
            }

            await context.SaveChangesAsync(cancellationToken);
        }
    }

    private async static Task SeedIdentityResources(ConfigurationDbContext context, CancellationToken cancellationToken = default) 
    {
        if (!await context.IdentityResources.AnyAsync(cancellationToken))
        {
            foreach (var resource in IdentityConfig.GetIdentityResources())
            {
                await context.IdentityResources.AddAsync(resource.ToEntity(), cancellationToken);
            }

            await context.SaveChangesAsync(cancellationToken);
        }
    }

    private async static Task SeedApiScopes(ConfigurationDbContext context, CancellationToken cancellationToken = default)
    {
        if (!await context.ApiScopes.AnyAsync(cancellationToken))
        {
            foreach (var resource in IdentityConfig.GetApiScopes())
            {
                await context.ApiScopes.AddAsync(resource.ToEntity(), cancellationToken);
            }

            await context.SaveChangesAsync(cancellationToken);
        }
    }

    private async static Task SeedApiResources(ConfigurationDbContext context, CancellationToken cancellationToken = default)
    {
        if (!await context.ApiResources.AnyAsync(cancellationToken))
        {
            foreach (var resource in IdentityConfig.GetApiResources())
            {
                await context.ApiResources.AddAsync(resource.ToEntity(), cancellationToken);
            }

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}