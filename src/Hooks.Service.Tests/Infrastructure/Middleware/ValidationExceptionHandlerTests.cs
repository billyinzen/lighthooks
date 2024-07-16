using System.Text.Json;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Hooks.Service.Infrastructure.Middleware;
using Hooks.Service.Infrastructure.Middleware.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Hooks.Service.Tests.Infrastructure.Middleware;

public class ValidationExceptionHandlerTests
{
    private readonly ILogger<ValidationExceptionHandler> _logger = Substitute.For<ILogger<ValidationExceptionHandler>>();

    [Fact]
    public async Task TryHandleAsync_ReturnsFalse_WhenNotValidationException()
    {
        var exception = new Exception("generic exception");
        var sut = CreateSut();
        var actual = await sut.TryHandleAsync(new DefaultHttpContext(), exception, default);
        actual.Should().BeFalse();
    }
    
    [Fact]
    public async Task TryHandleAsync_ReturnsTrue_WhenValidationException()
    {
        using var stream = new MemoryStream();
        var httpContext = new DefaultHttpContext { Response = { Body = stream } };

        var exception = new ValidationException(new ValidationFailure[] {
            new("prop1", "error1"),
            new("prop1", "error2"),
            new("prop2", "error1")});
        
        var expected = ValidationErrorModel.FromException(exception);
        
        var sut = CreateSut();
        var actual = await sut.TryHandleAsync(httpContext, exception, default);

        actual.Should().BeTrue();
        
        var response = await ReadAsJsonAsync<ValidationErrorModel>(httpContext.Response.Body, default);
        response.Should().BeEquivalentTo(expected);
    }
    
    /*
     * Private method
     */

    private ValidationExceptionHandler CreateSut() 
        => new(_logger);

    private static async Task<T?> ReadAsJsonAsync<T>(Stream stream, CancellationToken cancellationToken)
    {
        stream.Position = 0;
        return await JsonSerializer.DeserializeAsync<T>(stream, options: null, cancellationToken);
    }
}