using System.Text.Json.Serialization;

namespace Hooks.Service.Controllers.RequestModels;

/// <summary>Model used when requesting the publication of a new webhook event.</summary>
/// <param name="CustomerId">The unique identifier of the customer for whom the message has been raised.</param>
/// <param name="EntityId">The unique identifier of the entity associated with this message.</param>
/// <param name="Topic">The topic of the webhook message in the format `entityType.eventType`.</param>
/// <param name="Uri">The URI of the location from which the user can fetch the information referenced in this webhook.</param>
/// <param name="OfficeIds">An optional collection of unique office identifiers to limit the scope of message delivery for a customer.</param>
public record PublishWebhookEventRequestModel(
    [property: JsonPropertyName("customerId")] string CustomerId, 
    [property: JsonPropertyName("entityId")] string EntityId, 
    [property: JsonPropertyName("topic")] string Topic, 
    [property: JsonPropertyName("uri")] string Uri, 
    [property: JsonPropertyName("officeIds")] IEnumerable<string>? OfficeIds = null);