namespace Hooks.Service.Infrastructure;

public static class SwaggerConfiguration
{
    public static WebApplicationBuilder AddConfiguredSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        return builder;
    }

    public static WebApplication UseConfiguredSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        return app;
    }
}