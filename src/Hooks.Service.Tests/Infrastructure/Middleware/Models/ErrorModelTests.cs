using FluentAssertions;
using Hooks.Service.Infrastructure.Middleware.Models;

namespace Hooks.Service.Tests.Infrastructure.Middleware.Models;

public class ErrorModelTests
{
    [Fact]
    public void FromException_ReturnsErrorModel_FromException()
    {
        const string exceptionMessage = "exception message";
        var exception = new Exception(exceptionMessage);

        var sut = ErrorModel.FromException(exception);

        sut.Status.Should().Be(500);
        sut.Detail.Should().Be(exceptionMessage);
        Uri.TryCreate(sut.Type, UriKind.Absolute, out _).Should().BeTrue();
    }
}