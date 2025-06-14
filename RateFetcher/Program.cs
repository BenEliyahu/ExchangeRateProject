using RateFetcher.Interfaces;
using RateFetcher.Models;
using RateFetcher.Services;
using RateFetcher.Workers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddSingleton<IExchangeRateFetcher, ExchangeRateFetcher>();
builder.Services.AddSingleton<IExchangeRateStorage, ExchangeRateStorage>();
builder.Services.AddHostedService<RateFetcherWorker>();
builder.Services.Configure<ExchangeRatesOptions>(builder.Configuration.GetSection("ExchangeRates"));

var app = builder.Build();

app.Run();
