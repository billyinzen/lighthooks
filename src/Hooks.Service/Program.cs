using Serilog;
using Hooks.Common;
using Hooks.Service.Infrastructure;

namespace Hooks.Service;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args)
            .AddConfiguredLogging()
            .AddConfiguredServices()
            .AddConfiguredSwagger()
            .AddConfiguredRouting();
        
        var app = builder.Build()
            .UseConfiguredLogging()
            .UseConfiguredSwagger()
            .UseConfiguredRouting();
        
        app.Run();
    }
}