using MediatR;

namespace TripService.Api.Features.Trips.Helpers.Delete;

public sealed record DeleteHelperCommand(Guid TripId, Guid HelperId) : IRequest<bool>;
