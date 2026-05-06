using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Api.Features.Customers.Get;

[ApiController]
[Route("api/customers")]
public sealed class GetCustomerController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetCustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>Gets a customer by ID.</summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetCustomerResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetCustomerQuery(id), cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }
}
