using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TruckService.Api.Features.TruckDocuments.GetAll;

[ApiController]
[Route("api/trucks/{truckId:guid}/documents")]
public sealed class GetAllTruckDocumentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllTruckDocumentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetAllTruckDocumentsResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(Guid truckId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllTruckDocumentsQuery(truckId), cancellationToken);
        return Ok(result);
    }
}
