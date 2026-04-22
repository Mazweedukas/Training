using System.Globalization;
using BankSystem.Services.Helpers;

namespace BankSystem.Services.Generators;

public class BasedOnTickUniqueNumberGenerator : IUniqueNumberGenerator
{
    public DateTime StartingPoint { get; set; }

    public BasedOnTickUniqueNumberGenerator(DateTime startingPoint)
    {
        this.StartingPoint = startingPoint;
    }

    public string Generate()
    {
        var elapsedTics = (DateTime.UtcNow - this.StartingPoint).Ticks;
        return elapsedTics.ToString(CultureInfo.InvariantCulture).GenerateHash();
    }
}
