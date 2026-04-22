using System.Text.Json.Serialization;

namespace CountryServices;

/// <summary>
/// Contains information about local currency of the country.
/// </summary>
internal sealed class LocalCurrencyInfo
{
    /// <summary>
    /// Gets or sets the country name.
    /// </summary>
    [JsonPropertyName("name")]
    public string? CountryName { get; set; }

    /// <summary>
    /// Gets or sets the currencies.
    /// </summary>
    [JsonPropertyName("currencies")]
#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
    public Currency[]? Currencies { get; set; }
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly

    /// <summary>
    /// Contains information about currency.
    /// </summary>
    public sealed class Currency
    {
        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        [JsonPropertyName("code")]
        public string? Code { get; set; }

        /// <summary>
        /// Gets or sets the currency name.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the currency symbol.
        /// </summary>
        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }
    }
}
