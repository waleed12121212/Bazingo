using Bazingo_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Core.Interfaces
{
    public interface IBidRepository
    {
        Task<Bid> GetBidByIdAsync(int bidId);
        Task<IEnumerable<Bid>> GetBidsByAuctionIdAsync(int auctionId);
        Task AddBidAsync(Bid bid);
        Task DeleteBidAsync(int bidId);
    }
}
