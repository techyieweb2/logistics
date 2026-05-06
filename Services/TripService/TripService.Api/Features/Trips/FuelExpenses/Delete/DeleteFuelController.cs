using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TripService.Api.Features.Trips.FuelExpenses.Delete;

[ApiController]
[Route("api/trips")]
public sealed class DeleteFuelController : ControllerBase
{
    private readonly IMediator _mediator;

    public DeleteFuelController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>Removes a fuel expense from a trip.</summary>
    [HttpDelete("{id:guid}/fuel/{fuelId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        Guid id,
        Guid fuelId,
        CancellationToken cancellationToken)
    {
        var deleted = await _mediator.Send(new DeleteFuelCommand(id, fuelId), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
