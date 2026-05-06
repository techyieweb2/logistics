using MediatR;

namespace TripService.Api.Features.Trips.GetAll;

public sealed record GetAllTripsQuery() : IRequest<IEnumerable<GetAllTripsResult>>;

public sealed record GetAllTripsResult(
    Guid Id,
    Guid DispatchId,
    string TruckPlateNumber,
    string DriverName,
    string CustomerName,
    string RouteName,
    string Origin,
    string Destination,
    decimal FreightCost,
    decimal DriverPay,
    string Status,
    DateTime CreatedAt
);
