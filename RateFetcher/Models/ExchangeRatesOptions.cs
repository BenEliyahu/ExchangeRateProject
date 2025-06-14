namespace RateFetcher.Models
{
    public class ExchangeRatesOptions
    {
        public string ApiKey { get; set; } = string.Empty;
        public Dictionary<string, string> CurrencyExchange { get; set; } = new();
    }

}
