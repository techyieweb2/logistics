using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Api.Features.Customers.Update;

[ApiController]
[Route("api/customers")]
public sealed class UpdateCustomerController : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateCustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>Updates an existing customer.</summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(UpdateCustomerResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateCustomerCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest("Route ID does not match the command ID.");

        var result = await _mediator.Send(command, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }
}
