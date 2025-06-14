using System.Text.Json;
using RatePrinter.Models;

namespace RatePrinter.Services
{
    public class ExchangeRateService
    {
        private readonly string _filePath;

        public ExchangeRateService()
        {
            var baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "Data");
            Directory.CreateDirectory(baseDir);
            _filePath = Path.Combine(baseDir, "exchangerates.json");
        }

        public async Task<List<ExchangeRate>> GetAllRatesAsync()
        {
            if (!File.Exists(_filePath))
                return new List<ExchangeRate>();

            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<ExchangeRate>>(json) ?? new List<ExchangeRate>();
        }

        public async Task<ExchangeRate?> GetRateByPairAsync(string pairName)
        {
            var rates = await GetAllRatesAsync();
            return rates.FirstOrDefault(r => r.PairName.Equals(pairName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
