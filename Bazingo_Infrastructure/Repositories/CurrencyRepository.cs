using Bazingo_Core.Entities;
using Bazingo_Core.Interfaces;
using Bazingo_Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bazingo_Infrastructure.Repositories
{
    public class CurrencyRepository : BaseRepository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Currency>> GetAllAsync()
        {
            return await _dbSet
                .Include(c => c.PriceHistories)
                .Where(c => !c.IsDeleted && c.IsActive)
                .OrderBy(c => c.Code)
                .ToListAsync();
        }

        public async Task<Currency> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(c => c.PriceHistories)
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        }

        public async Task<Currency> GetByCodeAsync(string code)
        {
            return await _dbSet
                .Include(c => c.PriceHistories)
                .FirstOrDefaultAsync(c => c.Code == code && !c.IsDeleted && c.IsActive);
        }

        public async Task<decimal> GetExchangeRateAsync(string fromCode, string toCode)
        {
            var fromCurrency = await GetByCodeAsync(fromCode);
            var toCurrency = await GetByCodeAsync(toCode);

            if (fromCurrency == null || toCurrency == null)
                throw new ArgumentException("One or both currencies not found");

            if (!fromCurrency.IsActive || !toCurrency.IsActive)
                throw new InvalidOperationException("One or both currencies are inactive");

            // Convert through base rate (assuming USD is base)
            return toCurrency.ExchangeRate / fromCurrency.ExchangeRate;
        }

        public async Task AddAsync(Currency currency)
        {
            currency.LastUpdated = DateTime.UtcNow;
            await base.AddAsync(currency);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Currency currency)
        {
            currency.LastUpdated = DateTime.UtcNow;
            await base.UpdateAsync(currency);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var currency = await GetByIdAsync(id);
            if (currency != null)
            {
                currency.IsDeleted = true;
                currency.IsActive = false;
                currency.LastUpdated = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
    }
}
