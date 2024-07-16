using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Hooks.Service.Controllers.RequestModels;
using Hooks.Service.Infrastructure.Middleware.Models;
using Hooks.Service.UseCases.PublishWebhookEvent;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace Hooks.Service.Tests.Integration;

public class MiddlewareIntegrationTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task Post_ReturnsUnprocessableEntity_WhenValidationFailed()
    {
        var client = factory.CreateClient();
        
        var model = new PublishWebhookEventRequestModel("customerId", "entityId", "invalid", "https://www.google.com");
        var response = await client.PostAsJsonAsync("/", model);

        response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
    }
    
    [Fact]
    public async Task Post_ReturnsInternalServerError_WhenUnhandledException()
    {
        var exception = new Exception("Exception McException Face");
        var expected = ErrorModel.FromException(exception);
        
        var mediator = Substitute.For<IMediator>();
        mediator.Send(Arg.Any<PublishWebhookEventCommand>(), Arg.Any<CancellationToken>())
            .ThrowsAsync(exception);
        
        var client = factory
            .WithWebHostBuilder(builder => 
                builder.ConfigureTestServices(services => 
                    services.RemoveAll(typeof(IMediator)).AddScoped<IMediator>(_ => mediator)))
            .CreateClient();
        
        var model = new PublishWebhookEventRequestModel("customerId", "entityId", "topic.one", "https://www.google.com");
        var response = await client.PostAsJsonAsync("/", model);

        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        var actual = await response.Content.ReadFromJsonAsync<ErrorModel>();
        actual.Should().NotBeNull();
        actual.Should().BeEquivalentTo(expected);
    }
}