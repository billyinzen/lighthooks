using Serilog;

namespace Hooks.Service.Infrastructure;

public static class LoggingConfiguration
{
    public static WebApplicationBuilder AddConfiguredLogging(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));
        return builder;
    }

    public static WebApplication UseConfiguredLogging(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        return app;
    }
}