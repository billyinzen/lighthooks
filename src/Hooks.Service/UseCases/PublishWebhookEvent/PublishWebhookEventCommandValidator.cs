using System.Text.RegularExpressions;
using FluentValidation;
using Hooks.Common.Enums;

namespace Hooks.Service.UseCases.PublishWebhookEvent;

public class PublishWebhookEventCommandValidator : AbstractValidator<PublishWebhookEventCommand>
{
    internal const string IncorrectFormat = "Inorrect format";
    internal const string UnsupportedOption = "Unsupported";
    public PublishWebhookEventCommandValidator()
    {
        // Topic must be in correct format
        var topicRegex = new Regex(@"^[a-z]+\.[a-z]+$");
        RuleFor(command => command.Topic)
            .Matches(topicRegex)
            .WithMessage(IncorrectFormat);

        // Entity type must be supported
        RuleFor(command => command.Topic)
            .Must(topic => EntityType.TryGetValue(topic.Split('.').ElementAtOrDefault(0), out _))
            .WithMessage(UnsupportedOption);
        
        // Event type must be supported
        RuleFor(command => command.Topic)
            .Must(topic => EventType.TryGetValue(topic.Split('.').ElementAtOrDefault(1), out _))
            .WithMessage(UnsupportedOption);
        
        // Uri must be valid
        RuleFor(command => command.Uri)
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .WithMessage(IncorrectFormat);

    }
}