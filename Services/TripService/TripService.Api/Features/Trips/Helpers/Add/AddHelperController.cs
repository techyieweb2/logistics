using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TripService.Api.Features.Trips.Helpers.Add;

[ApiController]
[Route("api/trips")]
public sealed class AddHelperController : ControllerBase
{
    private readonly IMediator _mediator;

    public AddHelperController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>Adds a helper to a trip.</summary>
    [HttpPost("{id:guid}/helpers")]
    [ProducesResponseType(typeof(AddHelperResult), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Add(
        Guid id,
        [FromBody] AddHelperCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.TripId)
            return BadRequest("Route ID does not match the command ID.");

        var result = await _mediator.Send(command, cancellationToken);
        return result is null ? NotFound() : CreatedAtAction(nameof(Add), new { id = result.Id }, result);
    }
}
