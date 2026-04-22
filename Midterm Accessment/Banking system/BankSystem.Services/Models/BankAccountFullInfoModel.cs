namespace BankSystem.Services.Models;

public class BankAccountFullInfoModel
{
    public string AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public int BankAccountId { get; set; }
    public int BonusPoints { get; set; }
    public string CurrencyCode { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
