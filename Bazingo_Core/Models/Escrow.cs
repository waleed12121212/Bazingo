using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Core.Models
{
    public class Escrow
    {
        public int EscrowID { get; set; }
        public int OrderID { get; set; }
        public Order Order { get; set; }
        public decimal Amount { get; set; }
        public EscrowStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public enum EscrowStatus
    {
        Held,
        Released,
        Refunded
    }
}
