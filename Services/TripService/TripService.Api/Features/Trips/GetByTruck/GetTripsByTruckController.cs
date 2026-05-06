using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TripService.Api.Features.Trips.GetByTruck;

[ApiController]
[Route("api/trips")]
public sealed class GetTripsByTruckController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetTripsByTruckController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Gets all trips for a specific truck in a given month and year.
    /// Used by ReportService to generate monthly truck reports.
    /// </summary>
    [HttpGet("by-truck/{truckId:guid}")]
    [ProducesResponseType(typeof(GetTripsByTruckResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByTruck(
        Guid truckId,
        [FromQuery] int month,
        [FromQuery] int year,
        CancellationToken cancellationToken)
    {
        if (month < 1 || month > 12)
            return BadRequest("Month must be between 1 and 12.");

        if (year < 2000 || year > DateTime.UtcNow.Year + 1)
            return BadRequest("Year is out of valid range.");

        var result = await _mediator.Send(
            new GetTripsByTruckQuery(truckId, month, year),
            cancellationToken);

        return Ok(result);
    }
}