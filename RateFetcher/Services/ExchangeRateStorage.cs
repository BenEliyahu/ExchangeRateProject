using RateFetcher.Interfaces;
using RateFetcher.Models;
using System.Text.Json;

namespace RateFetcher.Services
{
    public class ExchangeRateStorage : IExchangeRateStorage
    {
        private readonly string _filePath;

        public ExchangeRateStorage()
        {
            var baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "Data");
            Directory.CreateDirectory(baseDir);
            _filePath = Path.Combine(baseDir, "exchangerates.json");
        }

        public async Task SaveRatesAsync(List<ExchangeRate> rates)
        {
            var json = JsonSerializer.Serialize(rates, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePath, json);
        }

        public async Task<List<ExchangeRate>> LoadRatesAsync()
        {
            if (!File.Exists(_filePath))
                return new List<ExchangeRate>();

            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<ExchangeRate>>(json) ?? new List<ExchangeRate>();
        }
    }
}
