using MediatR;

namespace Hooks.Service.UseCases.PublishWebhookEvent;

/// <summary>Model used when requesting the publication of a new webhook event.</summary>
/// <param name="CustomerId">The unique identifier of the customer for whom the message has been raised.</param>
/// <param name="EntityId">The unique identifier of the entity associated with this message.</param>
/// <param name="Topic">The topic of the webhook message in the format `entityType.eventType`.</param>
/// <param name="Uri">The URI of the location from which the user can fetch the information referenced in this webhook.</param>
/// <param name="OfficeIds">An optional collection of unique office identifiers to limit the scope of message delivery for a customer.</param>
public record PublishWebhookEventCommand(
    string CustomerId,
    string EntityId,
    string Topic,
    string Uri,
    IEnumerable<string>? OfficeIds) : IRequest<bool>
{
    public string? EntityType 
        => Topic.Split('.').ElementAtOrDefault(0);
    
    public string? EventType 
        => Topic.Split('.').ElementAtOrDefault(1);
}