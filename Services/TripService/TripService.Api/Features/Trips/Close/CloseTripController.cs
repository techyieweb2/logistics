using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TripService.Api.Features.Trips.Close;

[ApiController]
[Route("api/trips")]
public sealed class CloseTripController : ControllerBase
{
    private readonly IMediator _mediator;

    public CloseTripController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>Closes a trip — no more expense changes allowed after this.</summary>
    [HttpPatch("{id:guid}/close")]
    [ProducesResponseType(typeof(CloseTripResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Close(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CloseTripCommand(id), cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }
}