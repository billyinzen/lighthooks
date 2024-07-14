using Hooks.Service.Controllers.RequestModels;
using Hooks.Service.UseCases.PublishWebhookEvent;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hooks.Service.Controllers;

[ApiController]
[Route("/")]
public class HooksController : ControllerBase
{
    private readonly IMediator _mediator;

    public HooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> PublishWebhookEvent([FromBody] PublishWebhookEventRequestModel model)
    {
        var command = new PublishWebhookEventCommand(model.CustomerId, model.EntityId, model.Topic, model.Uri, model.OfficeIds);
        var result = await _mediator.Send(command); 
        
        return result 
            ? Ok(model)
            : BadRequest();
    }
}