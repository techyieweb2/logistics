namespace TripService.Infrastructure.Kafka;

public sealed class KafkaSettings
{
    public string BootstrapServers { get; set; } = string.Empty;

    public static class Topics
    {
        public const string TripClosed = "trip.closed";
    }
}