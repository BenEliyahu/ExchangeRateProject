﻿namespace RateFetcher.Models;

public class ExchangeRate
{
    public string BaseCurrency { get; set; } = string.Empty;
    public string QuoteCurrency { get; set; } = string.Empty;
    public decimal Rate { get; set; }
    public DateTime LastUpdateTime { get; set; }

}
