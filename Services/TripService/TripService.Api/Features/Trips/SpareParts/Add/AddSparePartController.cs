using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TripService.Api.Features.Trips.SpareParts.Add;

[ApiController]
[Route("api/trips")]
public sealed class AddSparePartController : ControllerBase
{
    private readonly IMediator _mediator;

    public AddSparePartController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>Adds a spare part to a trip. TotalCost is auto-computed from Quantity x UnitCost.</summary>
    [HttpPost("{id:guid}/spare-parts")]
    [ProducesResponseType(typeof(AddSparePartResult), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Add(
        Guid id,
        [FromBody] AddSparePartCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.TripId)
            return BadRequest("Route ID does not match the command ID.");

        var result = await _mediator.Send(command, cancellationToken);
        return result is null ? NotFound() : CreatedAtAction(nameof(Add), new { id = result.Id }, result);
    }
}
