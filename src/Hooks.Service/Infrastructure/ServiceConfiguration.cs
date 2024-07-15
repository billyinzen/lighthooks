using FluentValidation;
using Hooks.Common;

namespace Hooks.Service.Infrastructure;

public static class ServiceConfiguration
{
    public static WebApplicationBuilder AddConfiguredServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
        builder.Services.AddValidatorsFromAssemblyContaining<Program>();
        
        // Use the common webhooks package
        builder.Services.AddHooksServices(builder.Configuration);
        
        return builder;
    }
}