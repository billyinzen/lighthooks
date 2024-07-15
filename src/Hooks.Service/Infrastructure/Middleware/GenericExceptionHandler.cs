using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Hooks.Service.Infrastructure.Middleware;

public class GenericExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GenericExceptionHandler> _logger;

    public GenericExceptionHandler(ILogger<GenericExceptionHandler> logger)
        => _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError("Exception occurred: {message}", exception.Message);

        var problem = new ProblemDetails
        {
            Status = 500,
            Title = "An error has occurred",
            Detail = exception.Message,
            Type = "http://reapit.hooks.service/problems/exception"
        };
        
        httpContext.Response.StatusCode = problem.Status.GetValueOrDefault();
        await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken: cancellationToken);

        return true;
    }
}