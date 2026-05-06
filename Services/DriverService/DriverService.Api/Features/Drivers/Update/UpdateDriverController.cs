using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DriverService.Api.Features.Drivers.Update;

[ApiController]
[Route("api/drivers")]
public sealed class UpdateDriverController : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateDriverController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Updates an existing driver.
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(UpdateDriverResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateDriverCommand command,
        CancellationToken cancellationToken)
    {
        // Ensure route id matches body command
        if (id != command.Id)
            return BadRequest("Route ID does not match the command ID.");

        var result = await _mediator.Send(command, cancellationToken);

        return result is null ? NotFound() : Ok(result);
    }
}