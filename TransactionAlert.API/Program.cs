using TransactionAlert.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<KafkaProducerService>();

var app = builder.Build();

app.MapControllers();
app.Run();