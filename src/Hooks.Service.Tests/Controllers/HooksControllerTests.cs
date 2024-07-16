using FluentAssertions;
using Hooks.Service.Controllers;
using Hooks.Service.Controllers.RequestModels;
using Hooks.Service.UseCases.PublishWebhookEvent;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace Hooks.Service.Tests.Controllers;

public class HooksControllerTests
{
    private readonly IMediator _mediator = Substitute.For<IMediator>();

    [Fact]
    public async Task PublishWebhookEvent_ReturnsNoContent_WhenMessageSent()
    {
        var model = new PublishWebhookEventRequestModel("customerId", "entityId", "topic.event", "uri", ["a", "b", "c"]);
        var expectedCommand = new PublishWebhookEventCommand(model.CustomerId, model.EntityId, model.Topic, model.Uri, model.OfficeIds);

        _mediator.Send(Arg.Any<PublishWebhookEventCommand>(), Arg.Any<CancellationToken>())
            .Returns(true);

        var sut = CreateSut();
        var actual = await sut.PublishWebhookEvent(model);
        
        await _mediator.Received(1).Send(expectedCommand, Arg.Any<CancellationToken>());

        var actualNoContent = actual as NoContentResult;
        actualNoContent.Should().NotBeNull();
        actualNoContent?.StatusCode.Should().Be(204);
    }


    [Fact]
    public async Task PublishWebhookEvent_ReturnsBadRequest_WhenMessageNotSent()
    {
        var model = new PublishWebhookEventRequestModel("customerId", "entityId", "topic.event", "uri", ["a", "b", "c"]);

        _mediator.Send(Arg.Any<PublishWebhookEventCommand>(), Arg.Any<CancellationToken>())
            .Returns(false);

        var sut = CreateSut();
        var actual = await sut.PublishWebhookEvent(model);
        
        var actualNoContent = actual as BadRequestResult;
        actualNoContent.Should().NotBeNull();
        actualNoContent?.StatusCode.Should().Be(400);
    }
    
    /*
     * Private methods
     */

    private HooksController CreateSut() 
        => new(_mediator);
}