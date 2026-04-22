using System.Globalization;
using System.Text.RegularExpressions;

namespace BankSystem.Services.Helpers;

public static class ValidatorService
{
    public static bool IsCurrencyValid(this string currency)
    {
        if (string.IsNullOrWhiteSpace(currency))
        {
            return false;
        }

        try
        {
            return CultureInfo
                .GetCultures(CultureTypes.SpecificCultures)
                .Select(c => new RegionInfo(c.Name))
                .Any(r => r.ISOCurrencySymbol.Equals(currency, StringComparison.OrdinalIgnoreCase));
        }
        catch (ArgumentException)
        {
            return false;
        }
    }

    public static bool IsEmailValid(this string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
    }
}
