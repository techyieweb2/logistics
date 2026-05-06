using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TruckService.Api.Features.TruckStatusHistories.Get;

[ApiController]
[Route("api/trucks/{truckId:guid}/status-histories")]
public sealed class GetTruckStatusHistoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetTruckStatusHistoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetTruckStatusHistoryResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid truckId, Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetTruckStatusHistoryQuery(truckId, id), cancellationToken);
        return Ok(result);
    }
}
