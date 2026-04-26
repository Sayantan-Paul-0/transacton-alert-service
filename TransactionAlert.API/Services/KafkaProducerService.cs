using Confluent.Kafka;
using System.Text.Json;
using TransactionAlert.Shared;

namespace TransactionAlert.API.Services;

public class KafkaProducerService
{
    private readonly IProducer<string, string> _producer;
    private const string Topic = "transactions";

    public KafkaProducerService(IConfiguration config)
    {
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = config["Kafka:BootstrapServers"]
        };
        _producer = new ProducerBuilder<string, string>(producerConfig).Build();
    }

    public async Task PublishTransactionAsync(Transaction transaction)
    {
        var message = new Message<string, string>
        {
            Key = transaction.Id.ToString(),
            Value = JsonSerializer.Serialize(transaction)
        };
        await _producer.ProduceAsync(Topic, message);
    }
}