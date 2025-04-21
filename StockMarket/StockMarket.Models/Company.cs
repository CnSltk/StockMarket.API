namespace StockMarket.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public int TradingRate { get; set; }
        public int TradingVolume { get; set; }
        public int TradingCost { get; set; }
        public string TradingCurrency { get; set; }
    }
}