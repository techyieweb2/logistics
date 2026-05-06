using DriverService.Api.Features.Drivers.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DriverService.Api.Features.Drivers.Get;

[ApiController]
[Route("api/drivers")]
public sealed class GetDriverController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetDriverController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Gets a driver by ID.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetDriverResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetDriverQuery(id), cancellationToken);

        return result is null ? NotFound() : Ok(result);
    }
}