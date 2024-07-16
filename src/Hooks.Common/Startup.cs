using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hooks.Common;

[ExcludeFromCodeCoverage]
public static class Startup
{
    public static IServiceCollection AddHooksServices(this IServiceCollection services, IConfiguration environment)
    {
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            var assembly = Assembly.GetEntryAssembly();
            x.AddConsumers(assembly);
            x.AddActivities(assembly);
            
            var awsConfig = environment.GetSection("AWS");
            x.UsingAmazonSqs((context, configuration) =>
            {
                configuration.Durable = true;
                configuration.AutoDelete = false;
                
                configuration.Host(awsConfig["REGION"],
                hostConfig =>
                {
                    hostConfig.AccessKey(awsConfig["ACCESS_KEY_ID"]);
                    hostConfig.SecretKey(awsConfig["SECRET_ACCESS_KEY"]);
                });
                
                configuration.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}