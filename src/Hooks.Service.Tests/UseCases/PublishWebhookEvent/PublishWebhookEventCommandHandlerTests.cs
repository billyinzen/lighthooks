using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Hooks.Common.Helpers;
using Hooks.Common.Messages;
using Hooks.Service.UseCases.PublishWebhookEvent;
using MassTransit;
using Microsoft.Extensions.Logging;
using NSubstitute;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Hooks.Service.Tests.UseCases.PublishWebhookEvent;

public class PublishWebhookEventCommandHandlerTests
{
    private readonly IBus _messageBus = Substitute.For<IBus>();
    private readonly IValidator<PublishWebhookEventCommand> _validator = Substitute.For<IValidator<PublishWebhookEventCommand>>();
    private readonly ILogger<PublishWebhookEventCommand> _logger = Substitute.For<ILogger<PublishWebhookEventCommand>>();

    [Fact]
    public async Task Handle_ThrowsValidationException_WhenValidationFailed()
    {
        var command = new PublishWebhookEventCommand(string.Empty, string.Empty, string.Empty, string.Empty, null);
        SetupValidator(false);
        
        var sut = CreateSut();
        var action = () => sut.Handle(command, default);
        await action.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Handle_PublishesMessage_WhenValidationPassed()
    {
        using var timeContext = new DateTimeOffsetProviderContext(DateTimeOffset.UnixEpoch);
            
        const string customerId = "customerId";
        const string entityId = "entityId";
        const string entityType = "entity";
        const string eventType = "event";
        const string uri = "uri";
        var officeIds = new[] {  "office1", "office2" };
        
        var command = new PublishWebhookEventCommand(customerId, entityId, $"{entityType}.{eventType}", uri, officeIds);
        SetupValidator(true);
        
        var sut = CreateSut();
        var actual = await sut.Handle(command, default);
        actual.Should().BeTrue();
        
        await _messageBus.Received(1)
            .Publish(Arg.Is<WebhookEvent>(evt =>
                evt.CustomerId == customerId
                && evt.EntityId == entityId
                && evt.EntityType == entityType
                && evt.EventType == eventType
                && evt.CallbackUri == uri
                && (evt.OfficeIds ?? Array.Empty<string>()).Equals(officeIds)), Arg.Any<CancellationToken>());
    }
    
    /*
     * Private methods
     */

    private PublishWebhookEventCommandHandler CreateSut() 
        => new(_messageBus, _validator, _logger);

    private void SetupValidator(bool isValid)
    {
        var result = new ValidationResult();
        if (!isValid)
            result.Errors.Add(new ValidationFailure("propertyName", "errorMessage"));
        
        _validator.ValidateAsync(Arg.Any<PublishWebhookEventCommand>(), Arg.Any<CancellationToken>())
            .Returns(result);
    }

}