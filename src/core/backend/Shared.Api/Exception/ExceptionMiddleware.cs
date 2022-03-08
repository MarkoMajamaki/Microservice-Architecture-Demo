using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Shared.Domain;

namespace Shared.Api;

public class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception x)
        {
            string title = null;
            if (x is DomainException domainException)
            {
                title = domainException.Title;
                context.Response.StatusCode = (int)domainException.StatusCode;
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.MultiStatus;
            }

            context.Response.ContentType = "application/json";

            var errorResponse = new {
                Title = title,
                StatusCodes = context.Response.StatusCode,
                Message = x.Message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    }
}