using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Veda.Application.SharedKernel.Exceptions;
using Veda.SharedKernel.Extensions;

namespace Veda.Api.MiddleWares;

public class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IWebHostEnvironment _environment;

    public ExceptionMiddleware(
        ILogger<ExceptionMiddleware> logger, IWebHostEnvironment environment)
    {
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e) when (e is DomainException or ApplicationException)
        {
            _logger.LogInformation(e, "Domain exception occurred with message: {ExceptionMessage}", e.Message);

            await Results.Problem(
                    detail: e.Message,
                    statusCode: StatusCodes.Status400BadRequest)
                .ExecuteAsync(context);
        }
        catch (DbUpdateException e) 
        {
            await Results.Problem(
                    detail: e.Message,
                    statusCode: StatusCodes.Status400BadRequest)
                .ExecuteAsync(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Could not process a request with exception: {e.Message}\n{e.StackTrace}");
            
            await Results.Problem(
                    _environment.IsDevelopment() ? e.WithStackTrace() : "Oops, we made a mistake",
                    "",
                    StatusCodes.Status500InternalServerError,
                    "An Error occurred",
                    "Error Type",
                    new Dictionary<string, object?>()
                    {
                        {"traceId", Activity.Current?.Id}
                    })
                .ExecuteAsync(context);
        }
    }
}