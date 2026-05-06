using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TruckService.Api.Features.TruckMaintenanceRecords.GetAll;

[ApiController]
[Route("api/trucks/{truckId:guid}/maintenance-records")]
public sealed class GetAllTruckMaintenanceRecordsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllTruckMaintenanceRecordsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetAllTruckMaintenanceRecordsResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(Guid truckId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllTruckMaintenanceRecordsQuery(truckId), cancellationToken);
        return Ok(result);
    }
}
