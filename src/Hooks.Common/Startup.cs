using System.Reflection;
using Amazon.SimpleNotificationService;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hooks.Common;

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

            // MassTransit doesn't work with SSO at the moment, so we'll get these from confih. You can either add them
            // to your appsettings (bad), or use secrets-manager. All the projects in this solution share a secrets key,
            // so open a terminal in Hooks.Common and use the following commands:
            // dotnet user-secrets set "AWS:REGION" "<region>"
            // dotnet user-secrets set "AWS:ACCESS_KEY_ID" "<access key>"
            // dotnet user-secrets set "AWS:SECRET_ACCESS_KEY" "<secret key>"
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