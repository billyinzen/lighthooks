using System.Diagnostics.CodeAnalysis;
using Hooks.Service.Infrastructure.Middleware.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Hooks.Service.Infrastructure.Examples;

/// <summary>
/// Example provider for the <see cref="ValidationErrorModelExampleProvider"/> class
/// </summary>
[ExcludeFromCodeCoverage]
public class ValidationErrorModelExampleProvider : IExamplesProvider<ValidationErrorModel>
{
    /// <inheritdoc />
    public ValidationErrorModel GetExamples()
        => new()
        {
            Status = 422,
            Title = "Validation failed",
            Detail = "One or more of the provided values have failed validation.",
            Type = "http://reapit.hooks.service/problems/validation",
            Errors = new Dictionary<string, IEnumerable<string>>
            {
                { "topic", [ "Topic \"properties.removed\" is not supported." ] },
                { "officeIds", [ "Office with Id \"OFF\" not found." ] }
            }
        };
}