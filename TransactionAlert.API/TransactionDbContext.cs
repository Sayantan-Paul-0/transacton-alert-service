using Microsoft.EntityFrameworkCore;
using TransactionAlert.Shared;

namespace TransactionAlert.API;

public class TransactionDbContext : DbContext
{
    public DbSet<TransactionRecord> Transactions { get; set; }

    public TransactionDbContext(DbContextOptions<TransactionDbContext> options)
        : base(options) { }
}