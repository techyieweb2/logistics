using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TruckService.Api.Features.TruckStatusHistories.GetAll;

[ApiController]
[Route("api/trucks/{truckId:guid}/status-histories")]
public sealed class GetAllTruckStatusHistoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllTruckStatusHistoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetAllTruckStatusHistoriesResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(Guid truckId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllTruckStatusHistoriesQuery(truckId), cancellationToken);
        return Ok(result);
    }
}
