using kafka_consumer.Datas;
using kafka_consumer.Entities;
using MassTransit;


namespace kafka_consumer.Consumers;
public class ProductConsumer : IConsumer<KafkaMessage>
{
    private readonly AppDbContext appDbContext;
    public ProductConsumer(AppDbContext dbContext)
    {
        appDbContext = dbContext;
    }
    public async Task Consume(ConsumeContext<KafkaMessage> context)
    {
        if (context.Message.Price < 0)
        {
            appDbContext.Remove(appDbContext.Products.First(x => x.Id == context.Message.Id));
        }
        else
        {
            var product = new Product()
            {
                Id = context.Message.Id,
                Name = context.Message.Name,
                Price = context.Message.Price
            };
            appDbContext.Add(product);
        }
        Console.WriteLine($"Consume Succesfully Id: {context.Message.Id}");
        await appDbContext.SaveChangesAsync();
    }
}