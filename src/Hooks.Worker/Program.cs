using Hooks.Worker.Infrastructure;

var builder = Host.CreateApplicationBuilder(args)
    .AddConfiguredLogging()
    .AddConfiguredServices();

var host = builder.Build();
host.Run();
