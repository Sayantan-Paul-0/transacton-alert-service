using System.ComponentModel.DataAnnotations;

namespace TransactionAlert.Shared;

public class TransactionRecord
{
    [Key]
    public Guid Id { get; set; }
    public string FromAccount { get; set; } = string.Empty;
    public string ToAccount { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "INR";
    public DateTime Timestamp { get; set; }
    public bool AlertTriggered { get; set; }
}