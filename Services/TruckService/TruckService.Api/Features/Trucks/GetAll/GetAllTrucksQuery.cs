using MediatR;

namespace TruckService.Api.Features.Trucks.GetAll;

public sealed record GetAllTrucksQuery : IRequest<IEnumerable<GetAllTrucksResult>>;

public sealed record GetAllTrucksResult(
    Guid Id,
    string PlateNumber,
    string? Brand,
    string? Model,
    int? YearModel,
    string? TruckType,
    string Status,
    DateTime CreatedAt
);
