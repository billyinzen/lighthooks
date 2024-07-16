using System.Diagnostics.CodeAnalysis;
using Swashbuckle.AspNetCore.Filters;

namespace Hooks.Service.Infrastructure;

/// <summary>Startup container extension methods for openapi services</summary>
[ExcludeFromCodeCoverage]
public static class SwaggerConfiguration
{
    /// <summary>Add open api services to the application builder</summary>
    /// <param name="builder">The web application builder.</param>
    public static WebApplicationBuilder AddConfiguredSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c => c.ExampleFilters());
        builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();
        return builder;
    }

    /// <summary>Add open api middleware to the application</summary>
    /// <param name="app">The application.</param>
    public static WebApplication UseConfiguredSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        return app;
    }
}