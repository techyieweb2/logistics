using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TruckService.Api.Features.TruckMaintenanceRecords.Get;

[ApiController]
[Route("api/trucks/{truckId:guid}/maintenance-records")]
public sealed class GetTruckMaintenanceRecordController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetTruckMaintenanceRecordController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetTruckMaintenanceRecordResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid truckId, Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetTruckMaintenanceRecordQuery(truckId, id), cancellationToken);
        return Ok(result);
    }
}
