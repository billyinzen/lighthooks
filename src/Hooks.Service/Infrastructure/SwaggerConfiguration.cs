using Swashbuckle.AspNetCore.Filters;

namespace Hooks.Service.Infrastructure;

public static class SwaggerConfiguration
{
    public static WebApplicationBuilder AddConfiguredSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c => c.ExampleFilters());
        builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();
        return builder;
    }

    public static WebApplication UseConfiguredSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        return app;
    }
}