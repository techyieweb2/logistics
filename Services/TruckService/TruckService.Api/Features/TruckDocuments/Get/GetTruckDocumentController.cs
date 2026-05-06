using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TruckService.Api.Features.TruckDocuments.Get;

[ApiController]
[Route("api/trucks/{truckId:guid}/documents")]
public sealed class GetTruckDocumentController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetTruckDocumentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetTruckDocumentResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid truckId, Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetTruckDocumentQuery(truckId, id), cancellationToken);
        return Ok(result);
    }
}
