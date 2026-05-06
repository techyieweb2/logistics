using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DispatcherService.Api.Features.Dispatches.Create;

[ApiController]
[Route("api/dispatches")]
public sealed class CreateDispatchController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreateDispatchController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new dispatch.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(CreateDispatchResult), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateDispatchCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
    }
}
