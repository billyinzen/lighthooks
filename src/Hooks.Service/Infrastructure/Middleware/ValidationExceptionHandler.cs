using FluentValidation;
using Hooks.Service.Infrastructure.Middleware.Models;
using Microsoft.AspNetCore.Diagnostics;

namespace Hooks.Service.Infrastructure.Middleware;

public class ValidationExceptionHandler : IExceptionHandler
{
    private readonly ILogger<ValidationExceptionHandler> _logger;

    public ValidationExceptionHandler(ILogger<ValidationExceptionHandler> logger)
        => _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ValidationException validationException)
            return false;
        
        _logger.LogInformation("Validation Failed: {message}", exception.Message);

        var problem = ValidationErrorModel.FromException(validationException);
        
        httpContext.Response.StatusCode = problem.Status;
        await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken: cancellationToken);

        return true;
    }
}