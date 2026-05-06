using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TruckService.Api.Features.Trucks.Create;

[ApiController]
[Route("api/trucks")]
public sealed class CreateTruckController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreateTruckController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateTruckResult), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateTruckCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
    }
}
