using System.Globalization;

namespace BankSystem.Services.Models;

public class AccountCashOperation
{
    public AccountCashOperation(decimal amount, DateTime date, string note)
    {
        this.Amount = amount;
        this.Date = date;
        this.Note = note;
    }

    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Note { get; set; }

    public override string ToString()
    {
        string formattedDate = this.Date.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

        if (this.Amount >= 0)
        {
            return $"{formattedDate} {this.Note} : Credited to account {this.Amount}.";
        }
        else
        {
            return $"{formattedDate} {this.Note} : Debited from account {this.Amount}.";
        }
    }
}
