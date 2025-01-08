using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Core.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public int BaseCurrencyID { get; set; }
        public Currency BaseCurrency { get; set; }
        public string SellerID { get; set; }
        public User Seller { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Images { get; set; } // JSON Array
        public string VideoURL { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<PriceHistory> PriceHistories { get; set; }
        public ICollection<Auction> Auctions { get; set; }
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
        public ICollection<ItemUnit> ItemUnits { get; set; }
    }

}
