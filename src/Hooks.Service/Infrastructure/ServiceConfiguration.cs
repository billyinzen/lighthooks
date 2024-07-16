using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Hooks.Common;

namespace Hooks.Service.Infrastructure;

/// <summary>Startup container extension methods for application services</summary>
[ExcludeFromCodeCoverage]
public static class ServiceConfiguration
{
    /// <summary>Add application services to the application builder</summary>
    /// <param name="builder">The web application builder.</param>
    public static WebApplicationBuilder AddConfiguredServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
        builder.Services.AddValidatorsFromAssemblyContaining<Program>();
        
        // Use the common webhooks package
        builder.Services.AddHooksServices(builder.Configuration);
        
        return builder;
    }
}