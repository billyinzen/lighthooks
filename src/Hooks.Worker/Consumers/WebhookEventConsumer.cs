using System.Text.Json;
using Hooks.Common.Messages;
using MassTransit;

namespace Hooks.Worker.Consumers;

public class WebhookEventConsumer : IConsumer<WebhookEvent>
{
    private readonly ILogger<WebhookEventConsumer> _logger;

    public WebhookEventConsumer(ILogger<WebhookEventConsumer> logger)
    {
        _logger = logger;
    }
    
    public Task Consume(ConsumeContext<WebhookEvent> context)
    {
        _logger.LogInformation(JsonSerializer.Serialize(context.Message));
        return Task.CompletedTask;
    }
}