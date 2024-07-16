using System.Diagnostics.CodeAnalysis;
using Hooks.Service.Controllers.RequestModels;
using Swashbuckle.AspNetCore.Filters;

namespace Hooks.Service.Infrastructure.Examples;

/// <summary>
/// Example provider for the <see cref="PublishWebhookEventRequestModel"/> class
/// </summary>
[ExcludeFromCodeCoverage]
public class PublishWebhookEventRequestModelExampleProvider : IExamplesProvider<PublishWebhookEventRequestModel>
{
    /// <inheritdoc />
    public PublishWebhookEventRequestModel GetExamples()
        => new (
            CustomerId: "RPT",
            EntityId: "BCK240006",
            Topic: "property.created",
            Uri: "https://webhook.recipient.net/customers/RPT",
            OfficeIds: ["OFF", "LON", "LDS"]);
}