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
    public class AuctionRepository : IAuctionRepository
    {
        private readonly ApplicationDbContext _context;

        public AuctionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Auction> GetAuctionByIdAsync(int auctionId)
        {
            return await _context.Auctions
                .Include(a => a.Product)
                .Include(a => a.Winner)
                .FirstOrDefaultAsync(a => a.AuctionID == auctionId);
        }

        public async Task<IEnumerable<Auction>> GetAllAuctionsAsync( )
        {
            return await _context.Auctions
                .Include(a => a.Product)
                .Include(a => a.Winner)
                .ToListAsync();
        }

        public async Task AddAuctionAsync(Auction auction)
        {
            await _context.Auctions.AddAsync(auction);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAuctionAsync(Auction auction)
        {
            _context.Auctions.Update(auction);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAuctionAsync(int auctionId)
        {
            var auction = await GetAuctionByIdAsync(auctionId);
            if (auction != null)
            {
                _context.Auctions.Remove(auction);
                await _context.SaveChangesAsync();
            }
        }
    }

}
