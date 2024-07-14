using System.Reflection;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Hooks.Common;

public static class Startup
{
    public static IServiceCollection AddHooksServices(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            var assembly = Assembly.GetEntryAssembly();
            x.AddConsumers(assembly);
            x.AddActivities(assembly);
            
            x.UsingInMemory((context, configuration) => configuration.ConfigureEndpoints(context));
        });

        return services;
    }
}