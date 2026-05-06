using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DispatcherService.Api.Features.Routes.Delete;

[ApiController]
[Route("api/routes")]
public sealed class DeleteRouteController : ControllerBase
{
    private readonly IMediator _mediator;

    public DeleteRouteController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Deletes a route by ID. Cannot delete routes with active dispatches.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _mediator.Send(new DeleteRouteCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
