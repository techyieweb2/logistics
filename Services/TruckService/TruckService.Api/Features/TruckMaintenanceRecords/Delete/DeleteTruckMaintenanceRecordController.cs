using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TruckService.Api.Features.TruckMaintenanceRecords.Delete;

[ApiController]
[Route("api/trucks/{truckId:guid}/maintenance-records")]
public sealed class DeleteTruckMaintenanceRecordController : ControllerBase
{
    private readonly IMediator _mediator;

    public DeleteTruckMaintenanceRecordController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(DeleteTruckMaintenanceRecordResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid truckId, Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteTruckMaintenanceRecordCommand(truckId, id), cancellationToken);
        return Ok(result);
    }
}
