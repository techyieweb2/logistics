using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Api.Features.Customers.Create;

[ApiController]
[Route("api/customers")]
public sealed class CreateCustomerController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreateCustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>Creates a new customer.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(CreateCustomerResult), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateCustomerCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
    }
}
