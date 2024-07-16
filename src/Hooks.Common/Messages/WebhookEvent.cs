using System.Text.Json.Serialization;
using Hooks.Common.Helpers;
using MassTransit;

namespace Hooks.Common.Messages;

/// <summary>Class representing the generic webhook event payload.</summary>
[EntityName("webhook-event")]
public class WebhookEvent
{
    /// <summary>The unique identifier of the customer for whom the message has been raised.</summary>
    public string CustomerId { get; private set; }
    
    /// <summary>An optional collection of unique office identifiers to limit the scope of message delivery for a customer. </summary>
    public IEnumerable<string>? OfficeIds { get; private set; }
    
    /// <summary>The unique identifier of the entity associated with this message.</summary>
    public string EntityId { get; private set; }
    
    /// <summary>The type of entity associated with this message.</summary>
    public string EntityType { get; private set; }
    
    /// <summary>The type of event associated with this message.</summary>
    public string EventType { get; private set; }
    
    /// <summary>The URI for the location from which the user can fetch the information referenced in this webhook.</summary>
    public string CallbackUri { get; private set; }
    
    /// <summary>The date and time on which this message was created.</summary>
    public DateTimeOffset EventTime { get; } = DateTimeOffsetProvider.Now;
    
    /// <summary>Initializes a new instance of <see cref="WebhookEvent"/></summary>
    /// <param name="customerId">The unique identifier of the customer for whom the message has been raised.</param>
    /// <param name="entityId">The unique identifier of the entity associated with this message.</param>
    /// <param name="entityType">The type of entity associated with this message.</param>
    /// <param name="eventType">The type of event associated with this message.</param>
    /// <param name="callbackUri">The URI for the location from which the user can fetch the information referenced in this webhook.</param>
    public WebhookEvent(string customerId, string entityId, string entityType, string eventType, string callbackUri)
    {
        CustomerId = customerId;
        EntityId = entityId;
        EntityType = entityType;
        EventType = eventType;
        CallbackUri = callbackUri;
    }

    /// <summary>Initializes a new instance of <see cref="WebhookEvent"/></summary>
    /// <param name="customerId">The unique identifier of the customer for whom the message has been raised.</param>
    /// <param name="officeIds">A collection of unique office identifiers to limit the scope of message delivery for a customer. </param>
    /// <param name="entityId">The unique identifier of the entity associated with this message.</param>
    /// <param name="entityType">The type of entity associated with this message.</param>
    /// <param name="eventType">The type of event associated with this message.</param>
    /// <param name="callbackUri">The URI for the location from which the user can fetch the information referenced in this webhook.</param>
    [JsonConstructor]
    public WebhookEvent(string customerId, IEnumerable<string> officeIds, string entityId, string entityType, string eventType, string callbackUri)
        : this(customerId, entityId, entityType, eventType, callbackUri)
    {
        OfficeIds = officeIds;
    }
};

