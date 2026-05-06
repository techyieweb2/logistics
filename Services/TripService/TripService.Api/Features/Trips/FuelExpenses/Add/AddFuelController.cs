using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TripService.Api.Features.Trips.FuelExpenses.Add;

[ApiController]
[Route("api/trips")]
public sealed class AddFuelController : ControllerBase
{
    private readonly IMediator _mediator;

    public AddFuelController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>Adds a fuel expense to a trip. Amount is auto-computed from Liters x PricePerLiter.</summary>
    [HttpPost("{id:guid}/fuel")]
    [ProducesResponseType(typeof(AddFuelResult), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Add(
        Guid id,
        [FromBody] AddFuelCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.TripId)
            return BadRequest("Route ID does not match the command ID.");

        var result = await _mediator.Send(command, cancellationToken);
        return result is null ? NotFound() : CreatedAtAction(nameof(Add), new { id = result.Id }, result);
    }
}
