using RateFetcher.Interfaces;

namespace RateFetcher.Workers
{
    public class RateFetcherWorker : BackgroundService
    {
        private readonly IExchangeRateFetcher _fetcher;
        private readonly IExchangeRateStorage _storage;
        private readonly ILogger<RateFetcherWorker> _logger;

        public RateFetcherWorker(IExchangeRateFetcher fetcher, IExchangeRateStorage storage, ILogger<RateFetcherWorker> logger)
        {
            _fetcher = fetcher;
            _storage = storage;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Fetching exchange rates...");
                var rates = await _fetcher.FetchRatesAsync();
                await _storage.SaveRatesAsync(rates);
                _logger.LogInformation("Exchange rates saved.");
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }
}
