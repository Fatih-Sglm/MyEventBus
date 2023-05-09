using Confluent.Kafka;
using MessageBrokers.Kafka.Extensions;
using MyEventBus.SampleApi.BgService;
using MessageBrokers.RabbitMq.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var congif = builder.Configuration.GetSection("KafkaConfig").Get<ConsumerConfig>();
//builder.Services.AddMyEventBusKafka(opt =>
//{
//    opt.BootstrapServers = congif.BootstrapServers;
//    opt.GroupId = congif.GroupId;
//});


//builder.Services.AddHostedService<KafkaBackgroundService>();

builder.Services.AddMyEventRabbitMq(builder.Configuration);

builder.Services.AddHostedService<RabbitMqBackgroundService>();

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
