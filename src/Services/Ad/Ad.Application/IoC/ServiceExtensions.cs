using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ad.Application.IoC;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddFluentValidation(config =>
        {
            config.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        });

        return services;
    }
}