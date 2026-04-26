namespace TransactionAlert.Shared;

public class Transaction
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FromAccount { get; set; } = string.Empty;
    public string ToAccount { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "INR";
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}