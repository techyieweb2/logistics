using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Api.Features.Customers.Delete;

[ApiController]
[Route("api/customers")]
public sealed class DeleteCustomerController : ControllerBase
{
    private readonly IMediator _mediator;

    public DeleteCustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>Deletes a customer by ID.</summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _mediator.Send(new DeleteCustomerCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
