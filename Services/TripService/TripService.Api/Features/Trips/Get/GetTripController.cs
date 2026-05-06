using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TripService.Api.Features.Trips.Get;

[ApiController]
[Route("api/trips")]
public sealed class GetTripController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetTripController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>Gets a trip by ID with all expenses.</summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetTripResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetTripQuery(id), cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }
}
