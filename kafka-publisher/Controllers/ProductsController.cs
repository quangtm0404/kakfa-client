using kafka_publisher.Producers;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace kafka_publisher.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{

    private readonly ITopicProducer<KafkaMessage> producer;
    public ProductsController(ITopicProducer<KafkaMessage> producer)
    {
        this.producer = producer;
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await producer.Produce(new()
        {
            Id = id,
            Price = -1
        }, cancellationToken: default);
        return NoContent();
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductCreateModel model)
    {
        await producer.Produce(new KafkaMessage
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Price = model.Price
        });

        return Ok();
    }

    public class ProductCreateModel
    {
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
    }
}