using kafka_consumer.Consumers;
using kafka_consumer.Datas;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase(databaseName: "ProductDb"));

// Add MassTransit
builder.Services.AddMassTransit(x =>
{
    x.UsingInMemory();
    x.AddRider(rider =>
    {
        
        rider.UsingKafka((context, k) =>
        {
            k.Host("localhost:9092");
            k.TopicEndpoint<KafkaMessage>("cool-topic", "helloworld", e =>
            {
               e.ConfigureConsumer<ProductConsumer>(context);
            });
        });
        
        rider.AddConsumer<ProductConsumer>();
        
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
