using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TripService.Api.Features.Trips.Helpers.Delete;

[ApiController]
[Route("api/trips")]
public sealed class DeleteHelperController : ControllerBase
{
    private readonly IMediator _mediator;

    public DeleteHelperController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>Removes a helper from a trip.</summary>
    [HttpDelete("{id:guid}/helpers/{helperId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        Guid id,
        Guid helperId,
        CancellationToken cancellationToken)
    {
        var deleted = await _mediator.Send(new DeleteHelperCommand(id, helperId), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
