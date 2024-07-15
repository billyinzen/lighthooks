using System.Text.Json;
using FluentValidation;
using Hooks.Common.Messages;
using MassTransit;
using MediatR;

namespace Hooks.Service.UseCases.PublishWebhookEvent;

public class PublishWebhookEventCommandHandler : IRequestHandler<PublishWebhookEventCommand, bool>
{
    private readonly IBus _messageBus;
    private readonly IValidator<PublishWebhookEventCommand> _validator;
    private readonly ILogger<PublishWebhookEventCommand> _logger;
    
    public PublishWebhookEventCommandHandler(
        IBus messageBus,
        IValidator<PublishWebhookEventCommand> validator, 
        ILogger<PublishWebhookEventCommand> logger)
    {
        _messageBus = messageBus;
        _validator = validator;
        _logger = logger;
    }

    public async Task<bool> Handle(PublishWebhookEventCommand request, CancellationToken cancellationToken)
    {
        // Validate the request, throwing ValidationException if failed
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        // Build the message object
        var topic = request.Topic.Split('.');
        var message = new WebhookEvent(
            customerId: request.CustomerId,
            officeIds: request.OfficeIds ?? Array.Empty<string>(), 
            entityId: request.EntityId, 
            entityType: topic.First(), 
            eventType: topic.Last(), 
            callbackUri: request.Uri);
        
        // Publish the message
        _logger.LogDebug("Writing Message to Queue: {json}", JsonSerializer.Serialize(message));
        await _messageBus.Publish(message, cancellationToken);
            
        return await Task.FromResult(true);
    }
}