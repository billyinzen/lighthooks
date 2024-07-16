using Hooks.Common;

namespace Hooks.Worker.Infrastructure;

public static class ServiceConfiguration
{
    public static HostApplicationBuilder AddConfiguredServices(this HostApplicationBuilder builder)
    {
        builder.Services.AddHooksServices(builder.Configuration);
        builder.Services.AddHostedService<Worker>();
        return builder;
    }
}