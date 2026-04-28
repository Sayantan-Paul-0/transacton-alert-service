using Microsoft.EntityFrameworkCore;
using TransactionAlert.Shared;

namespace TransactionAlert.Consumer;

public class TransactionDbContext : DbContext
{
    public DbSet<TransactionRecord> Transactions { get; set; }

    public TransactionDbContext(DbContextOptions<TransactionDbContext> options)
        : base(options) { }
}