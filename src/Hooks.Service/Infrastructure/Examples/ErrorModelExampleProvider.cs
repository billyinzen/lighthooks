using Hooks.Service.Infrastructure.Middleware.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Hooks.Service.Infrastructure.Examples;

public class ErrorModelExampleProvider : IExamplesProvider<ErrorModel>
{
    public ErrorModel GetExamples()
        => new()
        {
            Status = 500,
            Type = "http://reapit.hooks.service/problems/exception",
            Title = "An unexpected error has occurred",
            Detail = "Unable to connect to queue: \"<queue name>\""
        };
}