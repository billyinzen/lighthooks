using System.Text;
using System.Text.Json;
using FluentValidation;
using Hooks.Service.Infrastructure.Middleware.Models;
using Microsoft.AspNetCore.Diagnostics;

namespace Hooks.Service.Infrastructure.Middleware;

/// <summary>Exception handler for validation exceptions</summary>
/// <param name="logger">The logging service.</param>
public class ValidationExceptionHandler(ILogger<ValidationExceptionHandler> logger) 
    : IExceptionHandler
{
    /// <inheritdoc />
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ValidationException validationException)
            return false;
        
        logger.LogInformation("Validation Failed: {message}", exception.Message);

        var problem = ValidationErrorModel.FromException(validationException);
        
        httpContext.Response.StatusCode = problem.Status;
        await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);
        
        return true;
    }
}