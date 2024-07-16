using System.Diagnostics.CodeAnalysis;
using Hooks.Service.Infrastructure.Middleware;

namespace Hooks.Service.Infrastructure;

/// <summary>Startup container extension methods for routing services</summary>
[ExcludeFromCodeCoverage]
public static class RoutingConfiguration
{
    /// <summary>Add routing services to the application builder</summary>
    /// <param name="builder">The web application builder.</param>
    public static WebApplicationBuilder AddConfiguredRouting(this WebApplicationBuilder builder)
    {
        builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
        builder.Services.AddExceptionHandler<GenericExceptionHandler>();
        builder.Services.AddProblemDetails();
        builder.Services.AddControllers();
        
        return builder;
    }

    /// <summary>Add routing middleware to the application</summary>
    /// <param name="app">The application.</param>
    public static WebApplication UseConfiguredRouting(this WebApplication app)
    {
        app.UseExceptionHandler();
        app.UseHttpsRedirection();
        app.MapControllers();
        return app;
    }
}