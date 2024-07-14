using FluentValidation;

namespace Hooks.Service.Infrastructure;

public static class Startup
{
    public static IServiceCollection AddConfiguredServices(this IServiceCollection services)
    {
        // TODO: add serilog
        
        // Application services/repositories
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
        services.AddValidatorsFromAssemblyContaining<Program>();
        
        services.AddControllers();
        
        // Add swagger stuff
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        return services;
    }

    public static WebApplication UseConfiguredApplication(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
            return app;
        
        app.UseSwagger();
        app.UseSwaggerUI();
        
        app.UseHttpsRedirection();
        app.MapControllers();
        return app;
    }
}