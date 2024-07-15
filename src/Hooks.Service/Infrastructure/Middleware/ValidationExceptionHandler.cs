using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

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

        var errors = validationException.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                keySelector: grouping => grouping.Key,
                elementSelector: grouping => grouping.Select(failure => failure.ErrorMessage).Distinct());
        
        var problem = new ProblemDetails
        {
            Status = 422,
            Title = "Validation failed",
            Detail = "One or more of the provided values have failed validation.",
            Type = "http://reapit.hooks.service/problems/validation",
            Extensions = new Dictionary<string, object?>
            {
                { "Errors", errors }
            }
        };
        
        httpContext.Response.StatusCode = problem.Status.GetValueOrDefault();
        await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken: cancellationToken);

        return true;
    }
}