using Microsoft.AspNetCore.Builder;

namespace Exceptions.IoC;

public static class ApplicaitonBuilderExtensions
{
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        return app;
    }
}