using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TruckService.Api.Features.TruckDocuments.Delete;

[ApiController]
[Route("api/trucks/{truckId:guid}/documents")]
public sealed class DeleteTruckDocumentController : ControllerBase
{
    private readonly IMediator _mediator;

    public DeleteTruckDocumentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(DeleteTruckDocumentResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid truckId, Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteTruckDocumentCommand(truckId, id), cancellationToken);
        return Ok(result);
    }
}
