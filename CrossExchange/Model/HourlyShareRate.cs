using System;
using System.ComponentModel.DataAnnotations;

namespace CrossExchange
{
    public class HourlyShareRate
    {
        public int Id { get; set; }

        [Required]
        public DateTime TimeStamp { get; set; }

        [Required]
        public string Symbol { get; set; }

        [Required]
        public decimal Rate { get; set; }
    }
}
