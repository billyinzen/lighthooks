# Hooks Service

The Hooks.Service API exposes a single endpoint allowing users to push a message into the webhooks queue.

## Environment

We use MassTransit for queue communication, but it doesn't work with AWS SSO yet so we need to provide AWS credentials.

These can be added to the AWS key in appsettings, but this runs the risk of being accidentally commited.  Instead, we use the [Microsoft UserSecrets](https://www.nuget.org/packages/Microsoft.Extensions.Configuration.UserSecrets) package to store these credentials securely.

> Note: all projects in the Hooks solution share a secrets key, so the credentials need only be set once.

To add your credentials to the secrets manager, open a terminal in any of the non-test Hooks project directories and execute the following commands:

```sh
dotnet user-secrets set "AWS:REGION" "<region>"
dotnet user-secrets set "AWS:ACCESS_KEY_ID" "<access key>"
dotnet user-secrets set "AWS:SECRET_ACCESS_KEY" "<secret key>" 
```

## Endpoints

### `POST /`

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