using BankSystem.Services.Generators;

namespace BankSystem.Services.Models.Accounts;

public class SilverAccount : BankAccount
{
    private const int SilverBalanceCostPerPoint = 100;
    private const int SilverDepositCostPerPoint = 5;
    private const int SilverWithdrawCostPerPoint = 2;

    public SilverAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator generator)
        : base(owner, currencyCode, generator)
    {
    }

    public SilverAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator)
        : base(owner, currencyCode, numberGenerator)
    {
    }

    public SilverAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator generator, decimal initialBalance)
        : base(owner, currencyCode, generator, initialBalance)
    {
    }

    public SilverAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator, decimal initialBalance)
        : base(owner, currencyCode, numberGenerator, initialBalance)
    {
    }

    public override decimal Overdraft
    {
        get { return 2 * this.BonusPoints; }
    }

    protected override int CalculateDepositRewardPoints(decimal amount)
    {
        return (int)decimal.Max(decimal.Floor(this.Balance / SilverBalanceCostPerPoint) + decimal.Floor(amount / SilverDepositCostPerPoint), 0m);
    }

    protected override int CalculateWithdrawRewardPoints(decimal amount)
    {
        return (int)decimal.Max(decimal.Floor(this.Balance / SilverBalanceCostPerPoint) + decimal.Floor(amount / SilverWithdrawCostPerPoint), 0m);
    }
}
