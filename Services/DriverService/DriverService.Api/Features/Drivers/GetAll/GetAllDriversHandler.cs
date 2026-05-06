using DriverService.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DriverService.Api.Features.Drivers.GetAll;

public sealed class GetAllDriversHandler : IRequestHandler<GetAllDriversQuery, IEnumerable<GetAllDriversResult>>
{
    private readonly DriverDbContext _context;

    public GetAllDriversHandler(DriverDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetAllDriversResult>> Handle(GetAllDriversQuery query, CancellationToken cancellationToken)
    {
        return await _context.Drivers
            .AsNoTracking()
            .Select(d => new GetAllDriversResult(
                d.Id,
                d.FirstName,
                d.MiddleName,
                d.LastName,
                d.Gender,
                d.DateOfBirth,
                d.Height,
                d.Weight,
                d.Religion,
                d.CivilStatus,
                d.CivilStatusPlace,
                d.PhoneNumber,
                d.Email,
                d.LicenseNumber,
                d.SssNumber,
                d.PhilHealthNumber,
                d.PagIbigNumber,
                d.TinNumber,
                d.Status.ToString(),
                d.DateHired,
                d.CreatedAt
                ))
            .ToListAsync(cancellationToken);
    }
}