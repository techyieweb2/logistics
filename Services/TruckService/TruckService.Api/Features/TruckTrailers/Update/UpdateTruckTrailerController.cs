using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TruckService.Api.Features.TruckTrailers.Update;

[ApiController]
[Route("api/trucks/{truckId:guid}/trailers")]
public sealed class UpdateTruckTrailerController : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateTruckTrailerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(UpdateTruckTrailerResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        Guid truckId,
        Guid id,
        [FromBody] UpdateTruckTrailerCommand command,
        CancellationToken cancellationToken)
    {
        if (truckId != command.TruckId)
            return BadRequest("Route Truck ID does not match the request body Truck ID.");

        if (id != command.Id)
            return BadRequest("Route ID does not match the request body ID.");

        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}
