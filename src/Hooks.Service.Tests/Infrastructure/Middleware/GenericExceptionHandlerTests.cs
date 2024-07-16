using System.Text.Json;
using FluentAssertions;
using Hooks.Service.Infrastructure.Middleware;
using Hooks.Service.Infrastructure.Middleware.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Hooks.Service.Tests.Infrastructure.Middleware;

public class GenericExceptionHandlerTests
{
    private readonly ILogger<GenericExceptionHandler> _logger = Substitute.For<ILogger<GenericExceptionHandler>>();
    
    [Fact]
    public async Task TryHandleAsync_ReturnsTrue_WhenException()
    {
        using var stream = new MemoryStream();
        var httpContext = new DefaultHttpContext { Response = { Body = stream } };

        var exception = new Exception("exception message");
        
        var expected = ErrorModel.FromException(exception);
        
        var sut = CreateSut();
        var actual = await sut.TryHandleAsync(httpContext, exception, default);

        actual.Should().BeTrue();
        
        var response = await ReadAsJsonAsync<ErrorModel>(httpContext.Response.Body, default);
        response.Should().BeEquivalentTo(expected);
    }
    
    /*
     * Private method
     */

    private GenericExceptionHandler CreateSut() 
        => new(_logger);

    private static async Task<T?> ReadAsJsonAsync<T>(Stream stream, CancellationToken cancellationToken)
    {
        stream.Position = 0;
        return await JsonSerializer.DeserializeAsync<T>(stream, options: null, cancellationToken);
    }
}