namespace kafka_consumer.Entities;
public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
}