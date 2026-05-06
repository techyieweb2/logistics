using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TruckService.Api.Features.TruckTrailers.Delete;

[ApiController]
[Route("api/trucks/{truckId:guid}/trailers")]
public sealed class DeleteTruckTrailerController : ControllerBase
{
    private readonly IMediator _mediator;

    public DeleteTruckTrailerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(DeleteTruckTrailerResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid truckId, Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteTruckTrailerCommand(truckId, id), cancellationToken);
        return Ok(result);
    }
}
