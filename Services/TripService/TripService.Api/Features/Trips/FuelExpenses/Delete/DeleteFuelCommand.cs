using MediatR;

namespace TripService.Api.Features.Trips.FuelExpenses.Delete;

public sealed record DeleteFuelCommand(Guid TripId, Guid FuelId) : IRequest<bool>;
