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
    public class EscrowRepository : IEscrowRepository
    {
        private readonly ApplicationDbContext _context;

        public EscrowRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Add Escrow
        public async Task AddEscrowAsync(Escrow escrow)
        {
            await _context.Escrows.AddAsync(escrow);
            await _context.SaveChangesAsync();
        }

        // Get Escrow by ID
        public async Task<Escrow> GetEscrowByIdAsync(int escrowId)
        {
            return await _context.Escrows
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EscrowID == escrowId);
        }

        // Update Escrow
        public async Task UpdateEscrowAsync(Escrow escrow)
        {
            _context.Escrows.Update(escrow);
            await _context.SaveChangesAsync();
        }

        // Get All Escrows
        public async Task<IEnumerable<Escrow>> GetAllEscrowsAsync( )
        {
            return await _context.Escrows
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
