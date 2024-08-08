using kafka_client_api.Consumers;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
               e.ConfigureConsumer<KafkaMessageConsumer>(context);
            });
        });
        
        rider.AddConsumer<KafkaMessageConsumer>();
        
    });
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();


