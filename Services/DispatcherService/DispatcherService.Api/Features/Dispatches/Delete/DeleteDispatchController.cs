using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DispatcherService.Api.Features.Dispatches.Delete;

[ApiController]
[Route("api/dispatches")]
public sealed class DeleteDispatchController : ControllerBase
{
    private readonly IMediator _mediator;

    public DeleteDispatchController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Deletes a dispatch by ID. Only Pending or Cancelled dispatches can be deleted.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _mediator.Send(new DeleteDispatchCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
