using System.ComponentModel.DataAnnotations;

namespace CrossExchange
{
    public class TradeModel
    {
        [Required]
        public string Symbol { get; set; }

        [Required]
        public int NoOfShares { get; set; }

        [Required]
        public int PortfolioId { get; set; }

        [Required]
        [RegularExpression("BUY|SELL")]
        public string Action { get; set; }
    }
}