using System.Globalization;
using BankSystem.Services.Helpers;

namespace BankSystem.Services.Generators;

public class SimpleGenerator : IUniqueNumberGenerator
{
    private int lastNumber = 1234567890;

    public static SimpleGenerator Instance { get; } = new SimpleGenerator();

    private SimpleGenerator()
    {
    }

    public string Generate()
    {
        this.lastNumber++;

        return this.lastNumber.ToString(CultureInfo.InvariantCulture).GenerateHash();
    }
}
