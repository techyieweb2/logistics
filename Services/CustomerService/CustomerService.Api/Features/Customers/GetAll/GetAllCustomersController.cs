using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Api.Features.Customers.GetAll;

[ApiController]
[Route("api/customers")]
public sealed class GetAllCustomersController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllCustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>Gets all customers.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetAllCustomersResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllCustomersQuery(), cancellationToken);
        return Ok(result);
    }
}
