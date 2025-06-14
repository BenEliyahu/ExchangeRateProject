using RateFetcher.Models;

namespace RateFetcher.Interfaces
{
    public interface IExchangeRateStorage
    {
        Task SaveRatesAsync(List<ExchangeRate> rates);
        Task<List<ExchangeRate>> LoadRatesAsync();
    }

}
