using FluentValidation;

namespace Hooks.Service.Infrastructure.Middleware.Models;

/// <summary>
/// Model representing a validation error
/// </summary>
public class ValidationErrorModel : ErrorModel
{
    /// <summary>The validation errors</summary>
    public Dictionary<string, IEnumerable<string>> Errors { get; internal init; } = new();

    /// <summary>Creates a new instance of ValidationErrorModel representing a given <see cref="ValidationException"/> object.</summary>
    /// <param name="exception">The exception.</param>
    public static ValidationErrorModel FromException(ValidationException exception)
        => new()
        {
            Title = "Validation failed",
            Detail = "One or more of the provided values have failed validation.",
            Type = "http://reapit.hooks.service/problems/validation",
            Status = 422,
            Errors = exception.Errors
                .GroupBy(error => error.PropertyName)
                .ToDictionary(
                    keySelector: grouping => grouping.Key,
                    elementSelector: grouping => grouping.Select(failure => failure.ErrorMessage).Distinct())
        };
}