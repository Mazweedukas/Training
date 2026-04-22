using System.Net;
using System.Text.Json;

namespace CountryServices;

/// <summary>
/// Provides information about country local currency from RESTful API
/// <see><cref>https://restcountries.com/#api-endpoints-v2</cref></see>.
/// </summary>
public class CountryService : ICountryService
{
    private const string ServiceUrl = "https://restcountries.com/v2";

    private static readonly HttpClient HttpClient = new HttpClient();

#pragma warning disable SA1010 // Opening square brackets should be spaced correctly
    private readonly Dictionary<string, WeakReference<LocalCurrency>> currencyCountries = [];
#pragma warning restore SA1010 // Opening square brackets should be spaced correctly

    /// <summary>
    /// Gets information about currency by country code synchronously.
    /// </summary>
    /// <param name="alpha2Or3Code">ISO 3166-1 2-letter or 3-letter country code.</param>
    /// <see><cref>https://en.wikipedia.org/wiki/List_of_ISO_3166_country_codes</cref></see>
    /// <returns>Information about country currency as <see cref="LocalCurrency"/>>.</returns>
    /// <exception cref="ArgumentException">Throw if countryCode is null, empty, whitespace or invalid country code.</exception>
    public LocalCurrency GetLocalCurrencyByAlpha2Or3Code(string? alpha2Or3Code)
    {
        if (string.IsNullOrWhiteSpace(alpha2Or3Code))
        {
            throw new ArgumentException("Invalid country code", nameof(alpha2Or3Code));
        }

        using WebClient client = new WebClient();

        try
        {
            using Stream data = client.OpenRead($"{ServiceUrl}/alpha/{alpha2Or3Code}");

            LocalCurrencyInfo? currencyInfo = JsonSerializer.Deserialize<LocalCurrencyInfo>(data);

            LocalCurrency currency = new ();

            if (currencyInfo == null)
            {
                throw new ArgumentException("currencyInfo is null");
            }

            if (currencyInfo.Currencies == null || currencyInfo.Currencies.Length == 0)
            {
                throw new ArgumentException("there are no currencies");
            }

            currency.CountryName = currencyInfo.CountryName;
            currency.CurrencySymbol = currencyInfo.Currencies[0].Symbol;
            currency.CurrencyCode = currencyInfo.Currencies[0].Code;

            return currency;
        }
        catch (WebException ex)
        {
            throw new ArgumentException("Invalid country code", ex);
        }
        catch (JsonException ex)
        {
            throw new ArgumentException("Invalid JSON format", ex);
        }
    }

    /// <summary>
    /// Gets information about currency by country code asynchronously.
    /// </summary>
    /// <param name="alpha2Or3Code">ISO 3166-1 2-letter or 3-letter country code.</param>
    /// <see><cref>https://en.wikipedia.org/wiki/List_of_ISO_3166_country_codes</cref></see>.
    /// <param name="token">Token for cancellation asynchronous operation.</param>
    /// <returns>Information about country currency as <see cref="LocalCurrency"/>>.</returns>
    /// <exception cref="ArgumentException">Throw if countryCode is null, empty, whitespace or invalid country code.</exception>
    ///
    public Task<LocalCurrency> GetLocalCurrencyByAlpha2Or3CodeAsync(
    string? alpha2Or3Code,
    CancellationToken token)
    {
        if (string.IsNullOrWhiteSpace(alpha2Or3Code))
        {
            throw new ArgumentException("Invalid country code", nameof(alpha2Or3Code));
        }

        return GetLocalCurrencyInternalAsync(alpha2Or3Code, token);
    }

    /// <summary>
    /// Gets information about the country by the country capital synchronously.
    /// </summary>
    /// <param name="capital">Capital name.</param>
    /// <returns>Information about the country as <see cref="Country"/>>.</returns>
    /// <exception cref="ArgumentException">Throw if the capital name is null, empty, whitespace or nonexistent.</exception>
    public Country GetCountryInfoByCapital(string? capital)
    {
        if (string.IsNullOrWhiteSpace(capital))
        {
            throw new ArgumentException("capital is null", nameof(capital));
        }

        using WebClient client = new WebClient();

        try
        {
            using Stream data = client.OpenRead($"{ServiceUrl}/capital/{capital}");

#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
            CountryInfo[]? countryInfo = JsonSerializer.Deserialize<CountryInfo[]>(data);
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly

            if (countryInfo == null || countryInfo.Length == 0)
            {
                throw new ArgumentException("Didn't return an array", nameof(capital));
            }

            Country country = new ();

            CountryInfo realCountryInfo = countryInfo[0];

            country.Name = realCountryInfo.Name;
            country.CapitalName = realCountryInfo.CapitalName;
            country.Area = realCountryInfo.Area;
            country.Population = realCountryInfo.Population;

            if (realCountryInfo.Flags == null)
            {
                throw new ArgumentException("No flags present", nameof(capital));
            }

            country.Flag = realCountryInfo.Flags.Png ?? realCountryInfo.Flags.Svg;

            return country;
        }
        catch (WebException ex)
        {
            throw new ArgumentException("Invalid country code", ex);
        }
        catch (JsonException ex)
        {
            throw new ArgumentException("Invalid JSON format", ex);
        }
    }

    /// <summary>
    /// Gets information about the currency by the country capital asynchronously.
    /// </summary>
    /// <param name="capital">Capital name.</param>
    /// <param name="token">Token for cancellation asynchronous operation.</param>
    /// <returns>Information about the country as <see cref="Country"/>>.</returns>
    /// <exception cref="ArgumentException">Throw if the capital name is null, empty, whitespace or nonexistent.</exception>
    ///
    public Task<Country> GetCountryInfoByCapitalAsync(string? capital, CancellationToken token)
    {
        if (string.IsNullOrWhiteSpace(capital))
        {
            throw new ArgumentException("Invalid country code", nameof(capital));
        }

        return GetCountryInfoByCapitalInternalAsync(capital, token);
    }

    private static async Task<LocalCurrency> GetLocalCurrencyInternalAsync(
    string alpha2Or3Code,
    CancellationToken token)
    {
        try
        {
            Uri uri = new Uri($"{ServiceUrl}/alpha/{alpha2Or3Code}");
            var currencyInfo = await GetLocalCurrencyAsync(uri, token);

            if (currencyInfo.Currencies == null || currencyInfo.Currencies.Length == 0)
            {
                throw new InvalidOperationException("No currency data");
            }

            var currencyData = currencyInfo.Currencies[0];

            return new LocalCurrency
            {
                CountryName = currencyInfo.CountryName,
                CurrencySymbol = currencyData.Symbol,
                CurrencyCode = currencyData.Code,
            };
        }
        catch (HttpRequestException ex)
        {
            throw new ArgumentException("Invalid country code", ex);
        }
        catch (JsonException ex)
        {
            throw new ArgumentException("Invalid JSON format", ex);
        }
    }

    private static async Task<Country> GetCountryInfoByCapitalInternalAsync(string capital, CancellationToken token)
    {
        try
        {
            var uri = new Uri($"{ServiceUrl}/capital/{capital}");
            var countries = await GetCountryInfoAsync(uri, token);

            if (countries.Length == 0)
            {
                throw new InvalidOperationException("No country data returned");
            }

            var info = countries[0];

            if (info.Flags == null)
            {
                throw new InvalidOperationException("No flag data");
            }

            return new Country
            {
                Name = info.Name,
                CapitalName = info.CapitalName,
                Area = info.Area,
                Population = info.Population,
                Flag = info.Flags.Png ?? info.Flags.Svg,
            };
        }
        catch (HttpRequestException ex)
        {
            throw new ArgumentException("Invalid country code", ex);
        }
        catch (JsonException ex)
        {
            throw new ArgumentException("Invalid JSON format", ex);
        }
    }

    private static async Task<LocalCurrencyInfo> GetLocalCurrencyAsync(Uri uri, CancellationToken token)
    {
        using var stream = await HttpClient.GetStreamAsync(uri, token);

        var result = await JsonSerializer.DeserializeAsync<LocalCurrencyInfo>(
            stream,
            JsonSerializerOptions.Default,
            token);

        if (result == null)
        {
            throw new JsonException("Deserialization returned null");
        }

        return result;
    }

    private static async Task<CountryInfo[]> GetCountryInfoAsync(Uri uri, CancellationToken token)
    {
        using var stream = await HttpClient.GetStreamAsync(uri, token);

        var result = await JsonSerializer.DeserializeAsync<CountryInfo[]>(
            stream,
            JsonSerializerOptions.Default,
            token);

        if (result == null)
        {
            throw new JsonException("Deserialization returned null");
        }

        return result;
    }
}
