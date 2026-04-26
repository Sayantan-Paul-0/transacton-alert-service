namespace TransactionAlert.Shared;

public class TransactionAlert
{
    public Guid TransactionId { get; set; }
    public string Reason { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string FromAccount { get; set; } = string.Empty;
    public DateTime TriggeredAt { get; set; } = DateTime.UtcNow;
}