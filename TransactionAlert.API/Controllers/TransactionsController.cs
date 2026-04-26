using Microsoft.AspNetCore.Mvc;
using TransactionAlert.API.Services;
using TransactionAlert.Shared;

namespace TransactionAlert.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly KafkaProducerService _producer;

    public TransactionsController(KafkaProducerService producer)
    {
        _producer = producer;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Transaction transaction)
    {
        transaction.Id = Guid.NewGuid();
        transaction.Timestamp = DateTime.UtcNow;
        await _producer.PublishTransactionAsync(transaction);
        return Ok(new { message = "Transaction received", transactionId = transaction.Id });
    }
}