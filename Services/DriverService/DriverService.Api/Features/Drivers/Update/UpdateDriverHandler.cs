using DriverService.Domain.Enums;
using DriverService.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DriverService.Api.Features.Drivers.Update;

public sealed class UpdateDriverHandler : IRequestHandler<UpdateDriverCommand, UpdateDriverResult?>
{
    private readonly DriverDbContext _context;

    public UpdateDriverHandler(DriverDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateDriverResult?> Handle(UpdateDriverCommand command, CancellationToken cancellationToken)
    {
        var driver = await _context.Drivers
            .FirstOrDefaultAsync(d => d.Id == command.Id, cancellationToken);

        if (driver is null)
            return null;

        driver.FirstName = command.FirstName;
        driver.MiddleName = command.MiddleName;
        driver.LastName = command.LastName;
        driver.Gender = command.Gender;
        driver.DateOfBirth = command.DateOfBirth;
        driver.Height = command.Height;
        driver.Weight = command.Weight;
        driver.Religion = command.Religion;
        driver.CivilStatus = command.CivilStatus;
        driver.CivilStatusPlace = command.CivilStatusPlace;
        driver.PhoneNumber = command.PhoneNumber;
        driver.Email = command.Email;
        driver.LicenseNumber = command.LicenseNumber;
        driver.SssNumber = command.SssNumber;
        driver.PhilHealthNumber = command.PhilHealthNumber;
        driver.PagIbigNumber = command.PagIbigNumber;
        driver.TinNumber = command.TinNumber;
        driver.Status = Enum.Parse<DriverStatus>(command.Status);
        driver.DateHired = command.DateHired;
        driver.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateDriverResult(driver.Id, driver.FullName, driver.UpdatedAt!.Value);
    }
}