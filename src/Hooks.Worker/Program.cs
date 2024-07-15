using Hooks.Common;
using Hooks.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Configuration.AddConfiguration(new ConfigurationBuilder().AddEnvironmentVariables().Build());

builder.Services.AddHooksServices(builder.Configuration);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
