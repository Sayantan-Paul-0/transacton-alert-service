using Microsoft.EntityFrameworkCore;
using TransactionAlert.API;
using TransactionAlert.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<KafkaProducerService>();
builder.Services.AddDbContext<TransactionDbContext>(options =>
    options.UseSqlite("Data Source=../TransactionAlert.Consumer/transactions.db"));

var app = builder.Build();

app.MapControllers();
app.Run();