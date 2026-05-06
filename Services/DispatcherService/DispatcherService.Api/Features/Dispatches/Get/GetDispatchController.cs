using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DispatcherService.Api.Features.Dispatches.Get;

[ApiController]
[Route("api/dispatches")]
public sealed class GetDispatchController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetDispatchController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Gets a dispatch by ID.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetDispatchResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetDispatchQuery(id), cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }
}
