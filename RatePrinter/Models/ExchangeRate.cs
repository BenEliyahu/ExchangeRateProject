namespace RatePrinter.Models
{
    public class ExchangeRate
    {
        public string BaseCurrency { get; set; }
        public string QuoteCurrency { get; set; }
        public decimal Rate { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public string PairName { get; set; }
    }
}
