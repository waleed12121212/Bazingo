using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Core.Models
{
    public class Currency
    {
        public int CurrencyID { get; set; }
        public string Code { get; set; } // e.g., USD, EUR
        public string Symbol { get; set; } // e.g., $, €
        public decimal ExchangeRate { get; set; }
        public bool IsActive { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<User> Users { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<PriceHistory> PriceHistories { get; set; }
    }
}
