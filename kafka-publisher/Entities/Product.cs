namespace kafka_publisher.Entities;
public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
}