using Bazingo_Core.Entities;
using Bazingo_Core.Interfaces;
using Bazingo_Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bazingo_Core.Entities.Payment;
using Bazingo_Core.Entities.Shopping;

namespace Bazingo_Infrastructure.Repositories
{
    public class EscrowRepository : BaseRepository<EscrowTransaction>, IEscrowRepository
    {
        private readonly ApplicationDbContext _context;

        public EscrowRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<EscrowTransaction> GetEscrowByIdAsync(int id)
        {
            return await _dbSet
                .Include(e => e.Order)
                .Include(e => e.Buyer)
                .Include(e => e.Seller)
                .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
        }

        public async Task<IEnumerable<EscrowTransaction>> GetAllEscrowsAsync( )
        {
            return await _dbSet
                .Include(e => e.Order)
                .Include(e => e.Buyer)
                .Include(e => e.Seller)
                .Where(e => !e.IsDeleted)
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<EscrowTransaction>> GetEscrowsByBuyerIdAsync(string buyerId)
        {
            return await _dbSet
                .Include(e => e.Order)
                .Include(e => e.Seller)
                .Where(e => e.BuyerId == buyerId && !e.IsDeleted)
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<EscrowTransaction>> GetEscrowsBySellerIdAsync(string sellerId)
        {
            return await _dbSet
                .Include(e => e.Order)
                .Include(e => e.Buyer)
                .Where(e => e.SellerId == sellerId && !e.IsDeleted)
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<EscrowTransaction>> GetEscrowsByOrderIdAsync(int orderId)
        {
            return await _dbSet
                .Include(e => e.Buyer)
                .Include(e => e.Seller)
                .Where(e => e.OrderId == orderId && !e.IsDeleted)
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<EscrowTransaction>> GetEscrowsByStatusAsync(EscrowStatus status)
        {
            return await _dbSet
                .Include(e => e.Order)
                .Include(e => e.Buyer)
                .Include(e => e.Seller)
                .Where(e => e.Status == status && !e.IsDeleted)
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync();
        }

        public async Task AddEscrowAsync(EscrowTransaction escrow)
        {
            escrow.CreatedAt = DateTime.UtcNow;
            escrow.Status = EscrowStatus.Pending;
            await AddAsync(escrow);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEscrowAsync(EscrowTransaction escrow)
        {
            // Update timestamps based on status change
            if (escrow.Status == EscrowStatus.Released && !escrow.ReleasedAt.HasValue)
            {
                escrow.ReleasedAt = DateTime.UtcNow;
            }
            else if (escrow.Status == EscrowStatus.Refunded && !escrow.RefundedAt.HasValue)
            {
                escrow.RefundedAt = DateTime.UtcNow;
            }

            await UpdateAsync(escrow);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEscrowAsync(int id)
        {
            var escrow = await GetEscrowByIdAsync(id);
            if (escrow != null)
            {
                escrow.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
