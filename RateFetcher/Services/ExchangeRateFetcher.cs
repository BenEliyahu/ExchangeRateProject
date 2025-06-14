using Microsoft.Extensions.Options;
using RateFetcher.Interfaces;
using RateFetcher.Models;

namespace RateFetcher.Services;

public class ExchangeRateFetcher : IExchangeRateFetcher
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly Dictionary<string, string> _currencyExchange;

    public ExchangeRateFetcher(HttpClient httpClient, IOptions<ExchangeRatesOptions> options)
    {
        _httpClient = httpClient;
        _apiKey = options.Value.ApiKey;
        _currencyExchange = options.Value.CurrencyExchange;
    }

    public async Task<List<ExchangeRate>> FetchRatesAsync()
    {
        var rates = new List<ExchangeRate>();

        foreach (var pair in _currencyExchange)
        {
            var baseCurrency = pair.Key;
            var targetCurrencies = pair.Value;

            try
            {
                var apiUrl = $"https://api.fxratesapi.com/latest?api_key={_apiKey}&base={baseCurrency}&currencies={targetCurrencies}";
                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<FxRatesApiResponse>();
                    if (apiResponse?.Success == true)
                    {
                        foreach (var rate in apiResponse.Rates)
                        {
                            rates.Add(new ExchangeRate
                            {
                                BaseCurrency = baseCurrency,
                                QuoteCurrency = rate.Key,
                                Rate = rate.Value,
                                LastUpdateTime = DateTime.UtcNow
                            });
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"API request failed with status: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching rates for {baseCurrency}: {ex.Message}");
            }
        }

        return rates;
    }
}

