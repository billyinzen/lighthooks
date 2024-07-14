using Hooks.Common;
using Hooks.Service.Infrastructure;

namespace Hooks.Service;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddConfiguredServices()
            .AddHooksServices();
        
        var app = builder.Build();
        app.UseConfiguredApplication();
        
        app.Run();
    }
}