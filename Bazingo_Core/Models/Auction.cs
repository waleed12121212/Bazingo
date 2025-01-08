using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Core.Models
{
    public class Auction
    {
        public int AuctionID { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public decimal StartPrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public DateTime EndTime { get; set; }
        public string WinnerID { get; set; }
        public User Winner { get; set; }
        public ICollection<Bid> Bids { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
