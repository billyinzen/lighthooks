using Hooks.Service.Controllers.RequestModels;
using Hooks.Service.Infrastructure.Examples;
using Hooks.Service.Infrastructure.Middleware.Models;
using Hooks.Service.UseCases.PublishWebhookEvent;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace Hooks.Service.Controllers;

[ApiController]
[Route("/")]
public class HooksController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>Initialize a new instance of <see cref="HooksController"/></summary>
    /// <param name="mediator">The mediator service.</param>
    public HooksController(IMediator mediator)
        => _mediator = mediator;
    

    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ValidationErrorModel), 422)]
    [ProducesResponseType(typeof(ErrorModel), 500)]
    public async Task<IActionResult> PublishWebhookEvent([FromBody] PublishWebhookEventRequestModel model)
    {
        var command = new PublishWebhookEventCommand(model.CustomerId, model.EntityId, model.Topic, model.Uri, model.OfficeIds);
        var result = await _mediator.Send(command); 
        
        return result 
            ? NoContent()
            : BadRequest();
    }
}