using BankSystem.Services.Generators;

namespace BankSystem.Services.Models.Accounts;

public abstract class BankAccount
{
    private readonly List<AccountCashOperation> operations = new();

    protected BankAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator generator)
    {
        this.AccountOwner = owner ?? throw new ArgumentNullException(nameof(owner));
        this.CurrencyCode = currencyCode ?? throw new ArgumentNullException(nameof(currencyCode));
        this.Number = generator?.Generate() ?? throw new ArgumentNullException(nameof(generator));
    }

    protected BankAccount(AccountOwner owner, string currencyCode, Func<string> generator)
    {
        this.AccountOwner = owner ?? throw new ArgumentNullException(nameof(owner));
        this.CurrencyCode = currencyCode ?? throw new ArgumentNullException(nameof(currencyCode));
        this.Number = generator?.Invoke() ?? throw new ArgumentNullException(nameof(generator));
    }

    protected BankAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator generator, decimal initialBalance)
        : this(owner, currencyCode, generator)
    {
        if (initialBalance > 0)
        {
            this.Deposit(initialBalance, DateTime.Now, "Initial balance");
        }
    }

    protected BankAccount(AccountOwner owner, string currencyCode, Func<string> generator, decimal initialBalance)
        : this(owner, currencyCode, generator)
    {
        if (initialBalance > 0)
        {
            this.Deposit(initialBalance, DateTime.Now, "Initial balance");
        }
    }

    public string Number { get; protected set; }

    public decimal Balance { get; protected set; }

    public string CurrencyCode { get; protected set; }

    public AccountOwner AccountOwner { get; protected set; }

    public int BonusPoints { get; protected set; }

    public abstract decimal Overdraft { get; }

    public void Deposit(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be positive", nameof(amount));
        }

        this.Balance += amount;

        var points = this.CalculateDepositRewardPoints(amount);

        this.BonusPoints += points;

        this.CalculateDepositRewardPoints(amount);

        this.operations.Add(new AccountCashOperation(amount, date, note));
    }

    public void Withdraw(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be positive", nameof(amount));
        }

        if (this.Balance + this.Overdraft < amount)
        {
            throw new InvalidOperationException("Insufficient funds");
        }

        var points = this.CalculateWithdrawRewardPoints(amount);

        this.Balance -= amount;

        this.BonusPoints += points;

        this.operations.Add(new AccountCashOperation(-amount, date, note));
    }

    public IList<AccountCashOperation> Operations => this.operations;

#pragma warning disable CA1024 // Use properties where appropriate
    public IList<AccountCashOperation> GetAllOperations()
#pragma warning restore CA1024 // Use properties where appropriate
    {
        return this.operations;
    }

    public override string ToString()
    {
        return $"Account: {this.Number}, Balance: {this.Balance}, Currency: {this.CurrencyCode}, BonusPoints: {this.BonusPoints}";
    }

    protected abstract int CalculateDepositRewardPoints(decimal amount);

    protected abstract int CalculateWithdrawRewardPoints(decimal amount);
}
