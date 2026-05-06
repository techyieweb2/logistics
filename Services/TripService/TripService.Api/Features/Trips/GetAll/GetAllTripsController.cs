using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TripService.Api.Features.Trips.GetAll;

[ApiController]
[Route("api/trips")]
public sealed class GetAllTripsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllTripsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>Gets all trips.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetAllTripsResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllTripsQuery(), cancellationToken);
        return Ok(result);
    }
}