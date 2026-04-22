using BankSystem.EF.Entities;
using BankSystem.Services.Models;

namespace BankSystem.Services.Services;

public class OwnerService : BaseService
{
    public OwnerService(BankContext context) : base(context)
    {
    }

    public IReadOnlyList<AccountOwnerTotalBalanceModel> GetAccountOwnersTotalBalance()
    {
        return this.Context.BankAccounts
            .GroupBy(ba => new
            {
                ba.AccountOwnerId,
                ba.AccountOwner.FirstName,
                ba.AccountOwner.LastName,
                ba.CurrencyCode.CurrenciesCode
            })
            .Select(group => new AccountOwnerTotalBalanceModel
            {
                AccountOwnerId = group.Key.AccountOwnerId,
                FirstName = group.Key.FirstName,
                LastName = group.Key.LastName,
                CurrencyCode = group.Key.CurrenciesCode,

                Total = (decimal)group.Sum(x => (double)x.Balance)
            })
            .AsEnumerable()
            .OrderByDescending(x => x.Total)
            .ToList();
    }
}
