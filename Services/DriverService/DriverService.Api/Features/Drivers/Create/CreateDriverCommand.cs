using MediatR;

namespace DriverService.Api.Features.Drivers.Create;

public sealed record CreateDriverCommand(
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
    DateTime? DateHired
) : IRequest<CreateDriverResult>;

public sealed record CreateDriverResult(Guid Id, string FullName);