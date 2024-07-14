using MassTransit;

namespace Hooks.Worker.Consumers;

public class WebhookEventConsumerDefinition : ConsumerDefinition<WebhookEventConsumer>
{
    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator, 
        IConsumerConfigurator<WebhookEventConsumer> consumerConfigurator,
        IRegistrationContext context)
        => endpointConfigurator.UseMessageRetry(r => r.Interval(5, 1000));
    
}