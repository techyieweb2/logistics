using MediatR;

namespace DriverService.Api.Features.Drivers.Update;

public sealed record UpdateDriverCommand(
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
    DateTime? DateHired
) : IRequest<UpdateDriverResult?>;

public sealed record UpdateDriverResult(Guid Id, string FullName, DateTime UpdatedAt);