using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Hooks.Service.Infrastructure.Middleware.Models;

namespace Hooks.Service.Tests.Infrastructure.Middleware.Models;

public class ValidationErrorModelTests
{
    [Fact]
    public void FromException_ReturnsErrorModel_FromValidationException()
    {
        var failures = new[]
        {
            new ValidationFailure("prop1", "error1"),
            new ValidationFailure("prop1", "error2"),
            new ValidationFailure("prop2", "error1")
        };


        var errors = new Dictionary<string, string[]>
        {
            { "prop1", ["error1", "error2"] },
            { "prop2", ["error1"] }
        };
        
        var exception = new ValidationException(failures);

        var sut = ValidationErrorModel.FromException(exception);

        sut.Status.Should().Be(422);
        sut.Errors.Should().BeEquivalentTo(errors);
        Uri.TryCreate(sut.Type, UriKind.Absolute, out _).Should().BeTrue();
    }
}