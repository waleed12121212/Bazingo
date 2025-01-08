using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Core.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string BuyerID { get; set; }
        public User Buyer { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<OrderDetail> OrderDetails { get; set; }
        public Payment Payment { get; set; }
        public Shipping Shipping { get; set; }
        public Escrow Escrow { get; set; }
        public ICollection<Complaint> Complaints { get; set; }
    }
    public enum OrderStatus
    {
        Pending,
        Shipped,
        Completed,
        Canceled
    }
}
