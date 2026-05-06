using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TruckService.Api.Features.TruckDocuments.Update;

[ApiController]
[Route("api/trucks/{truckId:guid}/documents")]
public sealed class UpdateTruckDocumentController : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateTruckDocumentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(UpdateTruckDocumentResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        Guid truckId,
        Guid id,
        [FromBody] UpdateTruckDocumentCommand command,
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
