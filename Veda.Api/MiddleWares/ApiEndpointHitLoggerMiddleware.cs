using System.Security.Claims;

namespace Veda.Api.MiddleWares;

public class ApiEndpointHitLoggerMiddleware(
    ILogger<ApiEndpointHitLoggerMiddleware> logger) 
    : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        logger.LogTrace("Api endpoint hit {APIPath}, {User}", 
            context.Request.Path.Value,
            context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier));

        await next(context);
    }
}