using Serilog;

namespace Hooks.Worker.Infrastructure;

public static class LoggingConfiguration
{
    public static HostApplicationBuilder AddConfiguredLogging(this HostApplicationBuilder builder)
    {
        /*builder.Services.AddSerilog(lc => lc
           .MinimumLevel.Debug()
           .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
           .Enrich.FromLogContext()
           .WriteTo.File(Path.Join(builder.Environment.ContentRootPath, "myApp.log")));
        // builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
        */

        builder.Services.AddSerilog(configuration => configuration.ReadFrom.Configuration(builder.Configuration));
        
        return builder;
    }
}