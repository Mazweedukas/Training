using BankSystem.Services.Generators;

namespace BankSystem.Services.Models.Accounts;

public class GoldAccount : BankAccount
{
    private const int GoldBalanceCostPerPoint = 5;
    private const int GoldDepositCostPerPoint = 10;
    private const int GoldWithdrawCostPerPoint = 5;

    public GoldAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator generator)
        : base(owner, currencyCode, generator)
    {
    }

    public GoldAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator)
        : base(owner, currencyCode, numberGenerator)
    {
    }

    public GoldAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator generator, decimal initialBalance)
        : base(owner, currencyCode, generator, initialBalance)
    {
    }

    public GoldAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator, decimal initialBalance)
        : base(owner, currencyCode, numberGenerator, initialBalance)
    {
    }

    public override decimal Overdraft
    {
        get { return 3 * this.BonusPoints; }
    }

    protected override int CalculateDepositRewardPoints(decimal amount)
    {
        return (int)decimal.Max(decimal.Ceiling(this.Balance / GoldBalanceCostPerPoint) + decimal.Ceiling(amount / GoldDepositCostPerPoint), 0m);
    }

    protected override int CalculateWithdrawRewardPoints(decimal amount)
    {
        return (int)decimal.Max(decimal.Ceiling(this.Balance / GoldBalanceCostPerPoint) + decimal.Ceiling(amount / GoldWithdrawCostPerPoint), 0m);
    }
}
