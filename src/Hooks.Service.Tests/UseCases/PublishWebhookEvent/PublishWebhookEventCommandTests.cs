using FluentAssertions;
using Hooks.Service.UseCases.PublishWebhookEvent;

namespace Hooks.Service.Tests.UseCases.PublishWebhookEvent;

public class PublishWebhookEventCommandTests
{
    [Fact]
    public void EntityType_GetEntityType_FromTopic()
    {
        const string entityType = "property";
        const string eventType = "created";
        const string topic = $"{entityType}.{eventType}";

        var sut = CreateSut(topic: topic);
        sut.EntityType.Should().Be(entityType);
    }
    
    [Fact]
    public void EventType_GetEventType_FromTopic()
    {
        const string entityType = "property";
        const string eventType = "created";
        const string topic = $"{entityType}.{eventType}";

        var sut = CreateSut(topic: topic);
        sut.EventType.Should().Be(eventType);
    }
    
    /*
     * Private methods
     */

    private static PublishWebhookEventCommand CreateSut(
        string customerId = "customerId",
        string entityId = "entityId",
        string topic = "entity.event",
        string uri = "uri",
        IEnumerable<string>? officeIds = null)
        => new(customerId, entityId, topic, uri, officeIds);

}