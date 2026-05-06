using MediatR;

namespace ReportService.Api.Features.Reports.GenerateTruckMonthlyReport;

public sealed record GenerateTruckMonthlyReportQuery(
    Guid TruckId,
    int Month,
    int Year,
    string Format       // "pdf" or "excel"
) : IRequest<ReportFileResult>;

public sealed record ReportFileResult(
    byte[] FileContent,
    string ContentType,
    string FileName
);
