using MediatR;

namespace DriverService.Api.Features.Drivers.GetAll;

public sealed record GetAllDriversQuery() : IRequest<IEnumerable<GetAllDriversResult>>;

public sealed record GetAllDriversResult(
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