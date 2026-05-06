using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DriverService.Api.Features.Drivers.GetAll;

[ApiController]
[Route("api/drivers")]
public sealed class GetAllDriversController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllDriversController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Gets all drivers
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetAllDriversResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllDriversQuery(), cancellationToken);

        return result is null ? NotFound() : Ok(result);
    }
}