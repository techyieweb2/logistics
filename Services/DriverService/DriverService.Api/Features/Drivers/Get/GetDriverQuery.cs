using MediatR;

namespace DriverService.Api.Features.Drivers.Get;

public sealed record GetDriverQuery(Guid Id) : IRequest<GetDriverResult?>;

public sealed record GetDriverResult(
    Guid Id,
    string FirstName,
    string? MiddleName,
    string LastName,
    string? Gender,
    DateOnly? DateOfBirth,
    string? Height,
    string? Weight,
    string? Religion,
    string? CivilStatus,
    string? CivilStatusPlace,
    string? PhoneNumber,
    string? Email,
    string? LicenseNumber,
    string? SssNumber,
    string? PhilHealthNumber,
    string? PagIbigNumber,
    string? TinNumber,
    string Status,
    DateTime? DateHired,
    DateTime CreatedAt
);