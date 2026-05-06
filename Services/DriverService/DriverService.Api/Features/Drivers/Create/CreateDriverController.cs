using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DriverService.Api.Features.Drivers.Create
{
    [ApiController]
    [Route("api/drivers")]
    public sealed class CreateDriverController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CreateDriverController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateDriverResult), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(
            [FromBody] CreateDriverCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
        }
    }
}
