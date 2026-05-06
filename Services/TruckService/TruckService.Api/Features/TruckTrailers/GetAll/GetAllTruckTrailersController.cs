using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TruckService.Api.Features.TruckTrailers.GetAll;

[ApiController]
[Route("api/trucks/{truckId:guid}/trailers")]
public sealed class GetAllTruckTrailersController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllTruckTrailersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetAllTruckTrailersResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(Guid truckId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllTruckTrailersQuery(truckId), cancellationToken);
        return Ok(result);
    }
}
