namespace Hooks.Worker;

/// <summary>Worker service keeping application alive until a stop signal is received</summary>
/// <param name="logger">The service logger</param>
/// <remarks>Trying out primary constructor here, I'm not sure about it...</remarks>
public class Worker(ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Debug))
                logger.LogDebug("Worker running at: {time:s}", DateTimeOffset.Now);
            
            await Task.Delay(10000, stoppingToken);
        }
    }
}
