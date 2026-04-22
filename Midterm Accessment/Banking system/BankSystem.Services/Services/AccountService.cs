using BankSystem.EF.Entities;

namespace BankSystem.Services.Services;

public class AccountService : BaseService
{
    public AccountService(BankContext context) : base(context)
    {
    }

    public void AddAccount(BankAccount account)
    {
        this.Context.BankAccounts.Add(account);
        this.Context.SaveChanges();
    }

    public void AddCurrencyCode(CurrencyCode currencyCode)
    {
        this.Context.CurrencyCodes.Add(currencyCode);
        this.Context.SaveChanges();
    }

    public void DeleteAccount(BankAccount account)
    {
        this.Context.BankAccounts.Remove(account);
        this.Context.SaveChanges();
    }

    public void UpdateAccount(BankAccount account)
    {
        this.Context.BankAccounts.Update(account);
        this.Context.SaveChanges();
    }

    public IReadOnlyList<Models.BankAccountFullInfoModel> GetBankAccountsFullInfo()
    {

        return this.Context.BankAccounts
            .Select(ba => new Models.BankAccountFullInfoModel
            {
                AccountNumber = ba.Number,
                Balance = ba.Balance,
                BankAccountId = ba.Id,
                BonusPoints = ba.BonusPoints,
                CurrencyCode = ba.CurrencyCode.CurrenciesCode,
                FirstName = ba.AccountOwner.FirstName,
                LastName = ba.AccountOwner.LastName
            })
            .ToList();
    }
}
