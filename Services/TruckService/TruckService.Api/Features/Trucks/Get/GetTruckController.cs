using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TruckService.Api.Features.Trucks.Get;

[ApiController]
[Route("api/trucks")]
public sealed class GetTruckController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetTruckController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetTruckResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetTruckQuery(id), cancellationToken);
        return Ok(result);
    }
}
