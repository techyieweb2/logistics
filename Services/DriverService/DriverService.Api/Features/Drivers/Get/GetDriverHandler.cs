using DriverService.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DriverService.Api.Features.Drivers.Get;

public sealed class GetDriverHandler : IRequestHandler<GetDriverQuery, GetDriverResult?>
{
    private readonly DriverDbContext _context;

    public GetDriverHandler(DriverDbContext context)
    {
        _context = context;
    }

    public async Task<GetDriverResult?> Handle(GetDriverQuery query, CancellationToken cancellationToken)
    {
        var driver = await _context.Drivers
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == query.Id, cancellationToken);

        if (driver is null)
            return null;

        return new GetDriverResult(
            driver.Id,
            driver.FirstName,
            driver.MiddleName,
            driver.LastName,
            driver.Gender,
            driver.DateOfBirth,
            driver.Height,
            driver.Weight,
            driver.Religion,
            driver.CivilStatus,
            driver.CivilStatusPlace,
            driver.PhoneNumber,
            driver.Email,
            driver.LicenseNumber,
            driver.SssNumber,
            driver.PhilHealthNumber,
            driver.PagIbigNumber,
            driver.TinNumber,
            driver.Status.ToString(),
            driver.DateHired,
            driver.CreatedAt
        );
    }
}