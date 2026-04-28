using Microsoft.EntityFrameworkCore;
using TransactionAlert.Consumer;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<TransactionDbContext>(options =>
    options.UseSqlite("Data Source=transactions.db"));

builder.Services.AddSingleton<EmailService>();
builder.Services.AddHostedService<TransactionConsumerWorker>();

var host = builder.Build();

// Auto-create the database on startup
using (var scope = host.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TransactionDbContext>();
    db.Database.EnsureCreated();
}

host.Run();