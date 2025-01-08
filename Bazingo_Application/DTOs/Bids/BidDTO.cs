using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Application.DTOs.Bids
{
    public class BidDTO
    {
        public int BidID { get; set; }
        public int AuctionID { get; set; }
        public string UserID { get; set; }
        public decimal BidAmount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
