using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TripService.Api.Features.Trips.Expenses.Add;

[ApiController]
[Route("api/trips")]
public sealed class AddExpenseController : ControllerBase
{
    private readonly IMediator _mediator;

    public AddExpenseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>Adds a TollFee or Other expense to a trip.</summary>
    [HttpPost("{id:guid}/expenses")]
    [ProducesResponseType(typeof(AddExpenseResult), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Add(
        Guid id,
        [FromBody] AddExpenseCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.TripId)
            return BadRequest("Route ID does not match the command ID.");

        var result = await _mediator.Send(command, cancellationToken);
        return result is null ? NotFound() : CreatedAtAction(nameof(Add), new { id = result.Id }, result);
    }
}
