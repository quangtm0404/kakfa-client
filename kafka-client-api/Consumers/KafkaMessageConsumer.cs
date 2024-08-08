using MassTransit;

namespace kafka_client_api.Consumers;
class KafkaMessageConsumer : IConsumer<KafkaMessage>
{
    public Task Consume(ConsumeContext<KafkaMessage> context)
    {
        Console.WriteLine("Consume Message " + context.Message.Text);
        return Task.CompletedTask;
    }
}

public record KafkaMessage
{
    public string Text { get; init; } = string.Empty;
}