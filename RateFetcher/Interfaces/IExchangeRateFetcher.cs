using RateFetcher.Models;

namespace RateFetcher.Interfaces;

public interface IExchangeRateFetcher
{
    Task<List<ExchangeRate>> FetchRatesAsync();
}
