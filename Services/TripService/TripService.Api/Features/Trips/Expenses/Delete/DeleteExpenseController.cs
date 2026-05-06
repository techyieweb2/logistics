using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TripService.Api.Features.Trips.Expenses.Delete;

[ApiController]
[Route("api/trips")]
public sealed class DeleteExpenseController : ControllerBase
{
    private readonly IMediator _mediator;

    public DeleteExpenseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>Deletes an expense from a trip.</summary>
    [HttpDelete("{id:guid}/expenses/{expenseId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        Guid id,
        Guid expenseId,
        CancellationToken cancellationToken)
    {
        var deleted = await _mediator.Send(new DeleteExpenseCommand(id, expenseId), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
