using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DispatcherService.Api.Features.Dispatches.GetAll;

[ApiController]
[Route("api/dispatches")]
public sealed class GetAllDispatchesController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllDispatchesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Gets all dispatches.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetAllDispatchesResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllDispatchesQuery(), cancellationToken);
        return Ok(result);
    }
}
