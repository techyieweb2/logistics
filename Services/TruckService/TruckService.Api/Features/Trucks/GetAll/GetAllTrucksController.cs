using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TruckService.Api.Features.Trucks.GetAll;

[ApiController]
[Route("api/trucks")]
public sealed class GetAllTrucksController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllTrucksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetAllTrucksResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllTrucksQuery(), cancellationToken);
        return Ok(result);
    }
}
