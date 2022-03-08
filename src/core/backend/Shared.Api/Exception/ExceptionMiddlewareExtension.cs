using Microsoft.AspNetCore.Builder;

namespace Shared.Api;

public static class ExceptionMiddlewareExtension
{
    public static void ConfigExeptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}