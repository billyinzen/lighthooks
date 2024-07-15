using System.Text.RegularExpressions;
using FluentValidation;
using Hooks.Common.Enums;

namespace Hooks.Service.UseCases.PublishWebhookEvent;

public class PublishWebhookEventCommandValidator : AbstractValidator<PublishWebhookEventCommand>
{
    internal const string IncorrectFormat = "Incorrect format";
    internal const string UnsupportedTopic = "Unsupported topic";
    public PublishWebhookEventCommandValidator()
    {
        // Topic must be in correct format and each component must be supported
        var topicRegex = new Regex(@"^[a-z]+\.[a-z]+$");
        RuleFor(command => command.Topic)
            .Must(topic =>
                topicRegex.IsMatch(topic)
                && EntityType.TryGetValue(topic.Split('.').ElementAtOrDefault(0), out _)
                && EventType.TryGetValue(topic.Split('.').ElementAtOrDefault(1), out _)
            )
            .WithMessage(UnsupportedTopic);
        
        // Uri must be valid
        RuleFor(command => command.Uri)
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .WithMessage(IncorrectFormat);

    }
}