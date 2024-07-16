using FluentAssertions;
using Hooks.Service.UseCases.PublishWebhookEvent;

namespace Hooks.Service.Tests.UseCases.PublishWebhookEvent;

public class PublishWebhookEventCommandValidatorTests
{
    [Fact]
    public async Task Validator_Passes_WhenCommandValid()
    {
        var command = new PublishWebhookEventCommand("customerId", "entityId", "entity.event", "https://example.net", null);
        var sut = CreateSut();
        var actual = await sut.ValidateAsync(command);
        actual.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("includes.Capital")]
    [InlineData("has.three.parts")]
    [InlineData("singleton")]
    public async Task Validator_Fails_WhenTopicInvalid(string topic)
    {
        var command = new PublishWebhookEventCommand("customerId", "entityId", topic, "https://example.net", null);
        var sut = CreateSut();
        var actual = await sut.ValidateAsync(command);
        actual.IsValid.Should().BeFalse();

        var error = actual.Errors.Single();
        error.PropertyName.Should().BeEquivalentTo(nameof(PublishWebhookEventCommand.Topic));
        error.ErrorMessage.Should().BeEquivalentTo(PublishWebhookEventCommandValidator.IncorrectFormat);
    }
    
    [Fact]
    public async Task Validator_Fails_WhenUriInvalid()
    {
        var command = new PublishWebhookEventCommand("customerId", "entityId", "entity.event", "google.com", null);
        var sut = CreateSut();
        var actual = await sut.ValidateAsync(command);
        actual.IsValid.Should().BeFalse();

        var error = actual.Errors.Single();
        error.PropertyName.Should().BeEquivalentTo(nameof(PublishWebhookEventCommand.Uri));
        error.ErrorMessage.Should().BeEquivalentTo(PublishWebhookEventCommandValidator.IncorrectFormat);
    }
    
    /*
     * Private methods
     */

    private static PublishWebhookEventCommandValidator CreateSut() => new();
}