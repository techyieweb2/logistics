namespace ReportService.Infrastructure.Kafka;

public sealed class KafkaSettings
{
    public string BootstrapServers { get; set; } = string.Empty;
    public string ConsumerGroupId { get; set; } = "report-service";

    public static class Topics
    {
        public const string TripClosed = "trip.closed";
    }
}