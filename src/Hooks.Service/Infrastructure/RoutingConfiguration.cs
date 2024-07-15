using Hooks.Service.Infrastructure.Middleware;

namespace Hooks.Service.Infrastructure;

public static class RoutingConfiguration
{
    public static WebApplicationBuilder AddConfiguredRouting(this WebApplicationBuilder builder)
    {
        builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
        builder.Services.AddExceptionHandler<GenericExceptionHandler>();
        builder.Services.AddProblemDetails();
        builder.Services.AddControllers();
        
        return builder;
    }

    public static WebApplication UseConfiguredRouting(this WebApplication app)
    {
        app.UseExceptionHandler();
        app.UseHttpsRedirection();
        app.MapControllers();
        return app;
    }
}