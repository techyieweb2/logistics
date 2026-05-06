using MediatR;

namespace TripService.Api.Features.Trips.SpareParts.Delete;

public sealed record DeleteSparePartCommand(Guid TripId, Guid SparePartId) : IRequest<bool>;
