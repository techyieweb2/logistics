using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TruckService.Api.Features.TruckTrailers.Get;

[ApiController]
[Route("api/trucks/{truckId:guid}/trailers")]
public sealed class GetTruckTrailerController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetTruckTrailerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetTruckTrailerResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid truckId, Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetTruckTrailerQuery(truckId, id), cancellationToken);
        return Ok(result);
    }
}
