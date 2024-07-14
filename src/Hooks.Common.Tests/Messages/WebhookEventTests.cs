using Hooks.Common.Helpers;
using Hooks.Common.Messages;

namespace Hooks.Common.Tests.Messages;

public class WebhookEventTests
{
    // Ctor

    [Fact]
    public void Ctor_MapsProperties_FromParameters()
    {
        const string customerId = "customerId";
        const string entityId = "entityId";
        const string entityType = "entityType";
        const string eventType = "eventType";
        const string uri = "https://localhost.test";
        var fixedTime = new DateTimeOffset(2016, 4, 16, 10, 52, 13, TimeSpan.FromHours(-6));
        using var dateTimeContext = new DateTimeOffsetProviderContext(fixedTime);
        
        var sut = new WebhookEvent(customerId, entityId, entityType, eventType, uri);
        sut.CustomerId.Should().Be(customerId);
        sut.EntityId.Should().Be(entityId);
        sut.EntityType.Should().Be(entityType);
        sut.EventType.Should().Be(eventType);
        sut.EventTime.Should().Be(fixedTime);
        sut.OfficeIds.Should().BeNull();
        sut.CallbackUri.Should().Be(uri);
    }
    
    [Fact]
    public void Ctor_MapsProperties_FromParameters_WhenOfficeIdsProvided()
    {
        const string customerId = "customerId";
        var officeIds = new[] { "office1", "office2", "office3" };
        const string entityId = "entityId";
        const string entityType = "entityType";
        const string eventType = "eventType";
        const string uri = "https://localhost.test";
        var fixedTime = new DateTimeOffset(2020, 6, 20, 18, 05, 18, TimeSpan.FromHours(-5));
        using var dateTimeContext = new DateTimeOffsetProviderContext(fixedTime);
        
        var sut = new WebhookEvent(customerId, officeIds, entityId, entityType, eventType, uri);
        sut.CustomerId.Should().Be(customerId);
        sut.OfficeIds.Should().BeEquivalentTo(officeIds);
        sut.EntityId.Should().Be(entityId);
        sut.EntityType.Should().Be(entityType);
        sut.EventType.Should().Be(eventType);
        sut.EventTime.Should().Be(fixedTime);
        sut.CallbackUri.Should().Be(uri);
    } 
}