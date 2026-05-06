using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TruckService.Api.Features.TruckTrailers.Create;

[ApiController]
[Route("api/trucks/{truckId:guid}/trailers")]
public sealed class CreateTruckTrailerController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreateTruckTrailerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateTruckTrailerResult), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create(
        Guid truckId,
        [FromBody] CreateTruckTrailerCommand command,
        CancellationToken cancellationToken)
    {
        if (truckId != command.TruckId)
            return BadRequest("Route Truck ID does not match the request body Truck ID.");

        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(Create), new { truckId = result.TruckId, id = result.Id }, result);
    }
}
