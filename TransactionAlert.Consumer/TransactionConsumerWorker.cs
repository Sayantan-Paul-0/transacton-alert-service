using Confluent.Kafka;
using System.Text.Json;
using TransactionAlert.Shared;

namespace TransactionAlert.Consumer;

public class TransactionConsumerWorker : BackgroundService
{
    private readonly ILogger<TransactionConsumerWorker> _logger;
    private readonly IConfiguration _config;
    private readonly EmailService _emailService;
    private const string Topic = "transactions";
    private const decimal AlertThreshold = 10000;

    public TransactionConsumerWorker(
        ILogger<TransactionConsumerWorker> logger,
        IConfiguration config,
        EmailService emailService)
    {
        _logger = logger;
        _config = config;
        _emailService = emailService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = _config["Kafka:BootstrapServers"],
            GroupId = "transaction-alert-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
        consumer.Subscribe(Topic);

        _logger.LogInformation("Consumer started. Listening on topic: {Topic}", Topic);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var result = consumer.Consume(stoppingToken);
                var transaction = JsonSerializer.Deserialize<Transaction>(result.Message.Value);

                if (transaction == null) continue;

                _logger.LogInformation("Consumed transaction {Id} for amount {Amount}",
                    transaction.Id, transaction.Amount);

                if (transaction.Amount >= AlertThreshold)
                {
                    _logger.LogInformation("Alert threshold breached. Sending email...");

                    var subject = $"Alert: High Value Transaction Detected";
                    var body = $"""
                        Transaction Alert

                        Transaction ID : {transaction.Id}
                        From Account   : {transaction.FromAccount}
                        To Account     : {transaction.ToAccount}
                        Amount         : {transaction.Amount} {transaction.Currency}
                        Timestamp      : {transaction.Timestamp}

                        This transaction exceeded the alert threshold of {AlertThreshold} {transaction.Currency}.
                        """;

                    await _emailService.SendAlertAsync(_config["Email:AlertRecipient"]!, subject, body);
                    _logger.LogInformation("Alert email sent.");
                }
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message");
            }
        }

        consumer.Close();
    }
}