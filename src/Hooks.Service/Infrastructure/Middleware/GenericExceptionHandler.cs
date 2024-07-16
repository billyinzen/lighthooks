using Hooks.Service.Infrastructure.Middleware.Models;
using Microsoft.AspNetCore.Diagnostics;

namespace Hooks.Service.Infrastructure.Middleware;

public class GenericExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GenericExceptionHandler> _logger;

    public GenericExceptionHandler(ILogger<GenericExceptionHandler> logger)
        => _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError("Exception occurred: {message}", exception.Message);

        var problem = ErrorModel.FromException(exception);
        
        httpContext.Response.StatusCode = problem.Status;
        await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken: cancellationToken);

        return true;
    }
}