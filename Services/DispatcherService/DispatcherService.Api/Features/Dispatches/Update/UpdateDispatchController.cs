using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DispatcherService.Api.Features.Dispatches.Update;

[ApiController]
[Route("api/dispatches")]
public sealed class UpdateDispatchController : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateDispatchController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Updates an existing dispatch.
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(UpdateDispatchResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateDispatchCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest("Route ID does not match the command ID.");

        var result = await _mediator.Send(command, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }
}
