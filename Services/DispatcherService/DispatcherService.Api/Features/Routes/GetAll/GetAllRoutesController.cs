using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DispatcherService.Api.Features.Routes.GetAll;

[ApiController]
[Route("api/routes")]
public sealed class GetAllRoutesController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllRoutesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Gets all routes.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetAllRoutesResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllRoutesQuery(), cancellationToken);
        return Ok(result);
    }
}
