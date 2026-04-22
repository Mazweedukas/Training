using BankSystem.Services.Generators;

namespace BankSystem.Services.Models.Accounts;

public class StandardAccount : BankAccount
{
    private const int StandardBalanceCostPerPoint = 100;
    public StandardAccount(
        AccountOwner owner,
        string currencyCode,
        IUniqueNumberGenerator generator)
        : base(owner, currencyCode, generator)
    {
    }

    public StandardAccount(
    AccountOwner owner,
    string currencyCode,
    Func<string> numberGenerator)
    : base(owner, currencyCode, numberGenerator)
    {
    }

    public StandardAccount(
        AccountOwner owner,
        string currencyCode,
        IUniqueNumberGenerator generator,
        decimal initialBalance)
        : base(owner, currencyCode, generator, initialBalance)
    {
    }

    public StandardAccount(
        AccountOwner owner,
        string currencyCode,
        Func<string> numberGenerator,
        decimal initialBalance)
        : base(owner, currencyCode, numberGenerator, initialBalance)
    {
    }

    public override decimal Overdraft
    {
        get { return 0m; }
    }

    protected override int CalculateDepositRewardPoints(decimal amount)
    {
        return (int)Math.Max(decimal.Floor(this.Balance / StandardBalanceCostPerPoint), 0m);
    }

    protected override int CalculateWithdrawRewardPoints(decimal amount)
    {
        return (int)Math.Max(decimal.Floor(this.Balance / StandardBalanceCostPerPoint), 0m);
    }
}
