using Microsoft.EntityFrameworkCore;

namespace BankSystem.EF.Entities;

public class BankContext : DbContext
{
    public BankContext(DbContextOptions options)
        : base(options)
    {
        this.Database.EnsureCreated();
    }

    public DbSet<AccountCashOperation> AccountCashOperations { get; set; }

    public DbSet<AccountOwner> AccountOwners { get; set; }

    public DbSet<BankAccount> BankAccounts { get; set; }

    public DbSet<CurrencyCode> CurrencyCodes { get; set; }
}
