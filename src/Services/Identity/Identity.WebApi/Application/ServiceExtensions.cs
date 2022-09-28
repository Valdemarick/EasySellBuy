using FluentValidation.AspNetCore;
using Identity.WebApi.Application.Services;
using System.Reflection;

namespace Identity.WebApi.Application;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddFluentValidation(s => s.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}