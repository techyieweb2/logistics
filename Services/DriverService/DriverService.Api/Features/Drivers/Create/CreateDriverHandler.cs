using DriverService.Domain.Entities;
using DriverService.Domain.Enums;
using DriverService.Infrastructure.Persistence;
using MediatR;

namespace DriverService.Api.Features.Drivers.Create;

public sealed class CreateDriverHandler : IRequestHandler<CreateDriverCommand, CreateDriverResult>
{
    private readonly DriverDbContext _context;

    public CreateDriverHandler(DriverDbContext context)
    {
        _context = context;
    }

    public async Task<CreateDriverResult> Handle(CreateDriverCommand command, CancellationToken cancellationToken)
    {
        var driver = new Driver
        {
            FirstName = command.FirstName,
            MiddleName = command.MiddleName,
            LastName = command.LastName,
            Gender = command.Gender,
            DateOfBirth = command.DateOfBirth,
            Height = command.Height,
            Weight = command.Weight,
            Religion = command.Religion,
            CivilStatus = command.CivilStatus,
            CivilStatusPlace = command.CivilStatusPlace,
            PhoneNumber = command.PhoneNumber,
            Email = command.Email,
            LicenseNumber = command.LicenseNumber,
            SssNumber = command.SssNumber,
            PhilHealthNumber = command.PhilHealthNumber,
            PagIbigNumber = command.PagIbigNumber,
            TinNumber = command.TinNumber,
            Status = DriverStatus.Active,
            DateHired = command.DateHired,
            CreatedAt = DateTime.UtcNow
        };

        _context.Drivers.Add(driver);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateDriverResult(driver.Id, driver.FullName);
    }
}