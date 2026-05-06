using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ReportService.Api.Features.Reports.GenerateTruckMonthlyReport;

[ApiController]
[Route("api/reports")]
public sealed class GenerateTruckMonthlyReportController : ControllerBase
{
    private readonly IMediator _mediator;

    public GenerateTruckMonthlyReportController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Generates a monthly trip report for a specific truck.
    /// Format: "pdf" or "excel"
    /// Example: GET /api/reports/trucks/{truckId}/monthly?month=4&year=2026&format=pdf
    /// </summary>
    [HttpGet("trucks/{truckId:guid}/monthly")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GenerateMonthlyReport(
        Guid truckId,
        [FromQuery] int month,
        [FromQuery] int year,
        [FromQuery] string format = "pdf",
        CancellationToken cancellationToken = default)
    {
        if (month < 1 || month > 12)
            return BadRequest("Month must be between 1 and 12.");

        if (year < 2000 || year > DateTime.UtcNow.Year + 1)
            return BadRequest("Year is out of valid range.");

        if (format.ToLower() is not ("pdf" or "excel"))
            return BadRequest("Format must be 'pdf' or 'excel'.");

        var result = await _mediator.Send(
            new GenerateTruckMonthlyReportQuery(truckId, month, year, format),
            cancellationToken);

        return File(result.FileContent, result.ContentType, result.FileName);
    }
}
