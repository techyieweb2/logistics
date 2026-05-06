using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TripService.Api.Features.Trips.UpdateDriverPay;

[ApiController]
[Route("api/trips")]
public sealed class UpdateDriverPayController : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateDriverPayController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>Updates the driver pay for a trip.</summary>
    [HttpPatch("{id:guid}/driver-pay")]
    [ProducesResponseType(typeof(UpdateDriverPayResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateDriverPay(
        Guid id,
        [FromBody] UpdateDriverPayCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.TripId)
            return BadRequest("Route ID does not match the command ID.");

        var result = await _mediator.Send(command, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }
}
