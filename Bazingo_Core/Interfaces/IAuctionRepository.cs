using Bazingo_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Core.Interfaces
{
    public interface IAuctionRepository
    {
        Task<Auction> GetAuctionByIdAsync(int auctionId);
        Task<IEnumerable<Auction>> GetAllAuctionsAsync( );
        Task AddAuctionAsync(Auction auction);
        Task UpdateAuctionAsync(Auction auction);
        Task DeleteAuctionAsync(int auctionId);
    }
}
