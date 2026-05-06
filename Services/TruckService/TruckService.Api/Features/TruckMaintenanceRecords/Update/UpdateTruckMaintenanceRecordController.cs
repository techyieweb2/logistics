using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TruckService.Api.Features.TruckMaintenanceRecords.Update;

[ApiController]
[Route("api/trucks/{truckId:guid}/maintenance-records")]
public sealed class UpdateTruckMaintenanceRecordController : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateTruckMaintenanceRecordController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(UpdateTruckMaintenanceRecordResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        Guid truckId,
        Guid id,
        [FromBody] UpdateTruckMaintenanceRecordCommand command,
        CancellationToken cancellationToken)
    {
        if (truckId != command.TruckId)
            return BadRequest("Route Truck ID does not match the request body Truck ID.");

        if (id != command.Id)
            return BadRequest("Route ID does not match the request body ID.");

        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}
