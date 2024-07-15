using Serilog;
using Hooks.Common;
using Hooks.Service.Infrastructure;

namespace Hooks.Service;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Configuration.AddConfiguration(new ConfigurationBuilder().AddEnvironmentVariables().Build());

        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));
        
        builder.Services.AddConfiguredServices()
            .AddHooksServices(builder.Configuration);
        
        var app = builder.Build();

        app.UseSerilogRequestLogging();
        app.UseConfiguredApplication();
        
        app.Run();
    }
}