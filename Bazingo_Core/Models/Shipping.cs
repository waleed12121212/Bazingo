using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Core.Models
{
    public class Shipping
    {
        public int ShippingID { get; set; }
        public int OrderID { get; set; }
        public Order Order { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string TrackingNumber { get; set; }
        public ShippingStatus ShippingStatus { get; set; }
        public string ShippingMethod { get; set; }
        public int ZoneID { get; set; }
        public Zone Zone { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    public enum ShippingStatus
    {
        Pending,
        InTransit,
        Delivered
    }
}
