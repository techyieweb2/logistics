using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Kernel.Events;
using TripService.Domain.Entities;
using TripService.Domain.Enums;
using TripService.Infrastructure.Kafka;
using TripService.Infrastructure.Persistence;

namespace TripService.Api.Features.Trips.Close;

public sealed class CloseTripHandler : IRequestHandler<CloseTripCommand, CloseTripResult?>
{
    private readonly TripDbContext _context;
    private readonly IKafkaProducerService _producer;

    public CloseTripHandler(TripDbContext context, IKafkaProducerService producer)
    {
        _context = context;
        _producer = producer;
    }

    public async Task<CloseTripResult?> Handle(CloseTripCommand command, CancellationToken cancellationToken)
    {
        // CloseTripHandler — fetch full trip with includes
        var trip = await _context.Trips
            .Include(t => t.Expenses)
            .Include(t => t.Helpers)
            .Include(t => t.FuelExpenses)
            .Include(t => t.SpareParts)
            .FirstOrDefaultAsync(t => t.Id == command.Id, cancellationToken);

        if (trip is null)
            return null;

        if (trip.Status == TripStatus.Closed)
            throw new InvalidOperationException("Trip is already closed.");

        trip.Status = TripStatus.Closed;
        trip.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        // CloseTripHandler — fetch full trip with includes
        var tripClosedEvent = new TripClosedEvent(
            TripId: trip.Id,
            DispatchId: trip.DispatchId,
            TruckId: trip.TruckId,
            TruckPlateNumber: trip.TruckPlateNumber,
            DriverId: trip.DriverId,
            DriverName: trip.DriverName,
            CustomerId: trip.CustomerId,
            CustomerName: trip.CustomerName,
            RouteName: trip.RouteName,
            Origin: trip.Origin,
            Destination: trip.Destination,
            FreightCost: trip.FreightCost,
            DriverPay: trip.DriverPay,
            Month: trip.UpdatedAt!.Value.Month,
            Year: trip.UpdatedAt!.Value.Year,
            ClosedAt: trip.UpdatedAt!.Value,
            Expenses: trip.Expenses.Select(e => new TripClosedExpenseItem(
                                  e.ExpenseType.ToString(), e.Description, e.Amount)),
            Helpers: trip.Helpers.Select(h => new TripClosedHelperItem(
                                  h.HelperName, h.Pay)),
            FuelExpenses: trip.FuelExpenses.Select(f => new TripClosedFuelItem(
                                  f.FuelType.ToString(), f.Liters, f.PricePerLiter, f.Amount, f.Station)),
            SpareParts: trip.SpareParts.Select(s => new TripClosedSparePartItem(
                                  s.PartName, s.Quantity, s.UnitCost, s.TotalCost))
        );

        await _producer.PublishAsync(
            topic: KafkaSettings.Topics.TripClosed,
            key: trip.Id.ToString(),
            message: tripClosedEvent,
            cancellationToken: cancellationToken
        );

        return new CloseTripResult(trip.Id, trip.Status.ToString(), trip.UpdatedAt!.Value);
    }
}