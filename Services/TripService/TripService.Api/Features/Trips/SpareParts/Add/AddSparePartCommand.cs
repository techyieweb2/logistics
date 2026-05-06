using MediatR;

namespace TripService.Api.Features.Trips.SpareParts.Add;

public sealed record AddSparePartCommand(
    Guid TripId,
    string PartName,
    int Quantity,
    decimal UnitCost
) : IRequest<AddSparePartResult?>;

public sealed record AddSparePartResult(
    Guid Id,
    Guid TripId,
    string PartName,
    int Quantity,
    decimal UnitCost,
    decimal TotalCost,
    DateTime CreatedAt
);
