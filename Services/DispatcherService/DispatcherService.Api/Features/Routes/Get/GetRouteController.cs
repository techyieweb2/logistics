using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DispatcherService.Api.Features.Routes.Get;

[ApiController]
[Route("api/routes")]
public sealed class GetRouteController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetRouteController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Gets a route by ID.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetRouteResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetRouteQuery(id), cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }
}
