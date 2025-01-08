using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Core.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserType UserType { get; set; } = UserType.Seller;
        public bool IsVerified { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public int? PreferredCurrencyID { get; set; }
        public Currency PreferredCurrency { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Product> Products { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Complaint> Complaints { get; set; }
        public ICollection<Bid> Bids { get; set; }
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }

        // تم حذف ActivityLogs
    }

    public enum UserType
    {
        Buyer,
        Seller
    }
}
