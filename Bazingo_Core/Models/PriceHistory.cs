using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Core.Models
{
    public class PriceHistory
    {
        public int PriceHistoryID { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public int CurrencyID { get; set; }
        public Currency Currency { get; set; }
        public decimal Price { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
    }
}
