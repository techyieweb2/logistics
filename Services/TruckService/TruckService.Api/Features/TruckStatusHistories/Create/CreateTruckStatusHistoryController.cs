using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TruckService.Api.Features.TruckStatusHistories.Create;

[ApiController]
[Route("api/trucks/{truckId:guid}/status-histories")]
public sealed class CreateTruckStatusHistoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreateTruckStatusHistoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateTruckStatusHistoryResult), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create(
        Guid truckId,
        [FromBody] CreateTruckStatusHistoryCommand command,
        CancellationToken cancellationToken)
    {
        if (truckId != command.TruckId)
            return BadRequest("Route Truck ID does not match the request body Truck ID.");

        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(Create), new { truckId = result.TruckId, id = result.Id }, result);
    }
}
