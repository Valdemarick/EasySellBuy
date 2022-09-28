using Identity.WebApi.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Identity.WebApi.Data;

public static class ServiceExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}