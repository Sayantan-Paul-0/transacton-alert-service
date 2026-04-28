using Microsoft.EntityFrameworkCore;
using TransactionAlert.API;
using TransactionAlert.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<KafkaProducerService>();
builder.Services.AddDbContext<TransactionDbContext>(options =>
    options.UseSqlite("Data Source=../TransactionAlert.Consumer/transactions.db"));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("AllowAngular");
app.MapControllers();
app.Run();