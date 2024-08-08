namespace kafka_consumer.Consumers;
public class KafkaMessage
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; } = 0;
}