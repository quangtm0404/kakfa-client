using kafka_consumer.Entities;
using Microsoft.EntityFrameworkCore;

namespace kafka_consumer.Datas;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {

    }
    public DbSet<Product> Products { get; set; }
}