using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TripService.Api.Features.Trips.Create;

[ApiController]
[Route("api/trips")]
public sealed class CreateTripController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreateTripController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new trip. DriverPay is pre-filled from Driver FixRate on the UI but editable.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(CreateTripResult), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateTripCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
    }
}
