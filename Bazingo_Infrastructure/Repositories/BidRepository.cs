using Bazingo.Infrastructure.Data;
using Bazingo_Core.Interfaces;
using Bazingo_Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Infrastructure.Repositories
{
    public class BidRepository : IBidRepository
    {
        private readonly ApplicationDbContext _context;

        public BidRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Bid> GetBidByIdAsync(int bidId)
        {
            return await _context.Bids.FindAsync(bidId);
        }

        public async Task<IEnumerable<Bid>> GetBidsByAuctionIdAsync(int auctionId)
        {
            return await _context.Bids.Where(b => b.AuctionID == auctionId).ToListAsync();
        }

        public async Task AddBidAsync(Bid bid)
        {
            await _context.Bids.AddAsync(bid);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBidAsync(int bidId)
        {
            var bid = await GetBidByIdAsync(bidId);
            if (bid != null)
            {
                _context.Bids.Remove(bid);
                await _context.SaveChangesAsync();
            }
        }
    }

}
