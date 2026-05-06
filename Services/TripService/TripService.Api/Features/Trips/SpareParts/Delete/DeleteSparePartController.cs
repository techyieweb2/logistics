using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TripService.Api.Features.Trips.SpareParts.Delete;

[ApiController]
[Route("api/trips")]
public sealed class DeleteSparePartController : ControllerBase
{
    private readonly IMediator _mediator;

    public DeleteSparePartController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>Removes a spare part from a trip.</summary>
    [HttpDelete("{id:guid}/spare-parts/{sparePartId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        Guid id,
        Guid sparePartId,
        CancellationToken cancellationToken)
    {
        var deleted = await _mediator.Send(new DeleteSparePartCommand(id, sparePartId), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
