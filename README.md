# ExchangeRateProject

## Overview

**ExchangeRateProject** is a .NET 9 based application designed to fetch live currency exchange rates from an external API and store them locally as a JSON file. It consists of two main components:

- **RateFetcher**: A background service that periodically retrieves exchange rates and saves them.
- **RatePrinter (API)**: A REST API that exposes the stored exchange rates with endpoints to retrieve all rates or specific currency pairs.

This project demonstrates clean architecture principles including interface abstraction, dependency injection, configuration management, and hosted background services using ASP.NET Core.

---

## Technologies Used

- .NET 9 (ASP.NET Core)
- C# 11
- BackgroundService for scheduled tasks
- HttpClient for API communication
- Dependency Injection (DI)
- JSON serialization with System.Text.Json
- Configuration via `appsettings.json`
- Logging with Microsoft.Extensions.Logging
- REST API using ASP.NET Core Minimal API / Controllers
- GitHub for version control

---

## Project Structure

### RateFetcher

- Implements `IExchangeRateFetcher` to fetch rates from [fxratesapi.com](https://fxratesapi.com).
- Implements `IExchangeRateStorage` to save and load rates locally as JSON.
- Contains `RateFetcherWorker` (a `BackgroundService`) that runs continuously and updates rates periodically.

### RatePrinter (API)

- Exposes API endpoints to serve exchange rates.
- Uses dependency injection to retrieve exchange rates from local storage.
- API Endpoints:
  - `GET /api/ExchangeRate` — Returns all stored exchange rates.
  - `GET /api/ExchangeRate/{pairName}` — Returns the exchange rate for a specific currency pair (e.g., `USDILS`).

---

## Configuration

Configure the API key and currency pairs in `appsettings.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ExchangeRates": {
    "ApiKey": "YOUR_API_KEY_HERE",
    "CurrencyExchange": {
      "USD": "ILS",
      "EUR": "ILS,USD,GBP",
      "GBP": "ILS,EUR"
    }
  }
}
```

- Replace `"YOUR_API_KEY_HERE"` with your actual API key from [fxratesapi.com](https://fxratesapi.com).
- Adjust the currency pairs as needed.

---

## How to Run

### 1. Clone the repository

```bash
git clone https://github.com/BenEliyahu/ExchangeRateProject.git
cd ExchangeRateProject
```

### 2. Run the RateFetcher background service

This service fetches exchange rates periodically and saves them locally.

```bash
cd RateFetcher
dotnet run
```

The fetcher runs indefinitely, updating rates every 10 seconds by default (this interval can be adjusted in the code).

---

### 3. Run the RatePrinter API

This service exposes stored exchange rates via REST API.

```bash
cd RatePrinter
dotnet run
```

The API will be available at `http://localhost:5000` (default port).

### Available Endpoints:

- `GET /api/ExchangeRate` — Retrieve all exchange rates.
- `GET /api/ExchangeRate/{pairName}` — Retrieve rate for a specific pair (e.g., `USDILS`).

---

### 4. Running both services together

Run `RateFetcher` and `RatePrinter` in separate terminal windows to have continuous fetching and API serving.

Alternatively, you can integrate the background fetcher into the API project as a hosted service to run both as a single application.

---

## Notes & Recommendations

- Exchange rates are stored locally in `Data/exchangerates.json`.
- The current fetch interval (10 seconds) is short for demo purposes — increase it for production.
- Basic error handling and console logging are implemented; consider enhancing logging for production.
- API key and currency pairs are loaded from configuration but could be further improved by using `IOptions<T>` patterns.
- Feel free to extend the API with additional features such as caching, authentication, or advanced filtering.

