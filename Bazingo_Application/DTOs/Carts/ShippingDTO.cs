using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Application.DTOs.Carts
{
    public class ShippingDTO
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string TrackingNumber { get; set; }
        public string ShippingStatus { get; set; }
    }
}
