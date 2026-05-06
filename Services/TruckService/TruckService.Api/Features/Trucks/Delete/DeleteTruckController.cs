using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TruckService.Api.Features.Trucks.Delete;

[ApiController]
[Route("api/trucks")]
public sealed class DeleteTruckController : ControllerBase
{
    private readonly IMediator _mediator;

    public DeleteTruckController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(DeleteTruckResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteTruckCommand(id), cancellationToken);
        return Ok(result);
    }
}
