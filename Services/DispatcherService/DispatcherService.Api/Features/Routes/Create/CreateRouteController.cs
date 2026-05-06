using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DispatcherService.Api.Features.Routes.Create;

[ApiController]
[Route("api/routes")]
public sealed class CreateRouteController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreateRouteController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new route.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(CreateRouteResult), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateRouteCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
    }
}
