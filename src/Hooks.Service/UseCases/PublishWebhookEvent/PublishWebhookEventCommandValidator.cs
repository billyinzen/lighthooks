using System.Text.RegularExpressions;
using FluentValidation;

namespace Hooks.Service.UseCases.PublishWebhookEvent;

public class PublishWebhookEventCommandValidator : AbstractValidator<PublishWebhookEventCommand>
{
    internal const string IncorrectFormat = "Incorrect format";
    public PublishWebhookEventCommandValidator()
    {
        // Topic must be in correct format and each component must be supported
        var topicRegex = new Regex(@"^[a-z]+\.[a-z]+$");
        RuleFor(command => command.Topic)
            .Must(topic => topicRegex.IsMatch(topic))
            .WithMessage(IncorrectFormat);
        
        // Uri must be valid
        RuleFor(command => command.Uri)
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .WithMessage(IncorrectFormat);

    }
}