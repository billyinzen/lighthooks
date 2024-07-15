# Hooks Service

The Hooks.Service API exposes a single endpoint allowing users to push a message into the webhooks queue.

## POST /

Push a message representing a system change to the webhooks queue.
> Note: `officeIds` may also be `null` or omitted entirely.

```json
// PublishWebhookEventRequestModel
{
    "customerId": "BOB",
    "entityId": "LDN240012",
    "topic": "property.created",
    "uri": "https://www.webhook-recipient.net/customer/BOB",
    "officeIds": [
        "LDN",
        "BIR",
        "MAN"
    ]
}
```