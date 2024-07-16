using System.Diagnostics.CodeAnalysis;
using Serilog;

namespace Hooks.Service.Infrastructure;

/// <summary>Startup container extension methods for logging services</summary>
[ExcludeFromCodeCoverage]
public static class LoggingConfiguration
{
    /// <summary>Add logging services to the application builder</summary>
    /// <param name="builder">The web application builder.</param>
    public static WebApplicationBuilder AddConfiguredLogging(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));
        return builder;
    }

    /// <summary>Add logging middleware to the application</summary>
    /// <param name="app">The application.</param>
    public static WebApplication UseConfiguredLogging(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        return app;
    }
}