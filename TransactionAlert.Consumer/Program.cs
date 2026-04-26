using TransactionAlert.Consumer;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<EmailService>();
builder.Services.AddHostedService<TransactionConsumerWorker>();

var host = builder.Build();
host.Run();