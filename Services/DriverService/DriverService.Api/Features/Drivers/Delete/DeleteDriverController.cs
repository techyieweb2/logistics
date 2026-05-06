using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DriverService.Api.Features.Drivers.Delete;

[ApiController]
[Route("api/drivers")]
public sealed class DeleteDriverController : ControllerBase
{
    private readonly IMediator _mediator;

    public DeleteDriverController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Deletes a driver by ID.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken)
    {
        var deleted = await _mediator.Send(new DeleteDriverCommand(id), cancellationToken);

        return deleted ? NoContent() : NotFound();
    }
}