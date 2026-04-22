using BankSystem.Services.Helpers;
using BankSystem.Services.Models.Accounts;

namespace BankSystem.Services.Models;

public class AccountOwner
{
    public AccountOwner(string firstName, string lastName, string email)
    {
        VerifyString(firstName, nameof(firstName));
        VerifyString(lastName, nameof(lastName));

        if (!email.IsEmailValid())
        {
            throw new ArgumentException(email, nameof(email));
        }

        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
        this.OwnedAccounts = new List<BankAccount>();
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

#pragma warning disable CA1859 // Use concrete types when possible for improved performance
    private IList<BankAccount> OwnedAccounts { get; }
#pragma warning restore CA1859 // Use concrete types when possible for improved performance

    public IList<BankAccount> Accounts()
    {
        return this.OwnedAccounts;
    }

    public void Add(BankAccount? account)
    {
        if (account != null)
        {
            this.OwnedAccounts.Add(account);
        }
    }

    public override string ToString()
    {
        return $"{this.FirstName} {this.LastName}, {this.Email}.";
    }

    private static void VerifyString(string value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Value cannot be null or empty", paramName);
        }
    }
}
