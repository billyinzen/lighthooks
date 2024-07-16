using Hooks.Service.Controllers.RequestModels;
using Swashbuckle.AspNetCore.Filters;

namespace Hooks.Service.Infrastructure.Examples;

public class PublishWebhookEventRequestModelExampleProvider : IExamplesProvider<PublishWebhookEventRequestModel>
{
    public PublishWebhookEventRequestModel GetExamples()
        => new (
            CustomerId: "RPT",
            EntityId: "BCK240006",
            Topic: "property.created",
            Uri: "https://webhook.recipient.net/customers/RPT",
            OfficeIds: ["OFF", "LON", "LDS"]);
}