using Hooks.Service.Infrastructure.Middleware.Models;
using Microsoft.AspNetCore.Diagnostics;

namespace Hooks.Service.Infrastructure.Middleware;

/// <summary>Exception handler for exceptions not caught by more specific handlers</summary>
/// <param name="logger">The logging service.</param>
public class GenericExceptionHandler(ILogger<GenericExceptionHandler> logger) 
    : IExceptionHandler
{
    /// <inheritdoc />
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError("Exception occurred: {message}", exception.Message);

        var problem = ErrorModel.FromException(exception);
        
        httpContext.Response.StatusCode = problem.Status;
        await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken: cancellationToken);

        return true;
    }
}