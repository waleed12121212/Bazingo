using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bazingo_Core.Entities.Product;
using Bazingo_Core.Interfaces;
using Bazingo_Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Bazingo_Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<ProductEntity>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<ProductEntity> _products;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
            _products = context.Set<ProductEntity>();
        }

        public async Task<ProductEntity> GetByIdAsync(int id)
        {
            return await _products
                .Include(p => p.Category)
                .Include(p => p.Images)
                .Include(p => p.Attributes)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }

        public async Task<IEnumerable<ProductEntity>> GetAllAsync()
        {
            return await _products
                .Include(p => p.Category)
                .Include(p => p.Images.Where(i => i.IsPrimary))
                .Where(p => !p.IsDeleted)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductEntity>> GetByCategoryAsync(int categoryId)
        {
            return await _products
                .Include(p => p.Images.Where(i => i.IsPrimary))
                .Where(p => p.CategoryId == categoryId && !p.IsDeleted)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductEntity>> GetBySellerAsync(string sellerId)
        {
            return await _products
                .Include(p => p.Category)
                .Include(p => p.Images.Where(i => i.IsPrimary))
                .Where(p => p.SellerId == sellerId && !p.IsDeleted)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<ProductEntity> AddAsync(ProductEntity product)
        {
            product.CreatedAt = DateTime.UtcNow;
            product.LastUpdated = DateTime.UtcNow;
            await _products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> UpdateAsync(ProductEntity product)
        {
            try
            {
                product.LastUpdated = DateTime.UtcNow;
                await base.UpdateAsync(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var product = await GetByIdAsync(id);
                if (product != null)
                {
                    product.IsDeleted = true;
                    product.LastUpdated = DateTime.UtcNow;
                    await base.UpdateAsync(product);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<ProductEntity>> SearchAsync(string keyword, int? categoryId = null)
        {
            var query = _products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(p =>
                    p.Name.Contains(keyword) ||
                    p.Description.Contains(keyword) ||
                    p.Brand.Contains(keyword));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            return await query
                .Include(p => p.Category)
                .Include(p => p.Images.Where(i => i.IsPrimary))
                .Where(p => !p.IsDeleted)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<bool> UpdateStockAsync(int productId, int quantity)
        {
            try
            {
                var product = await _products.FindAsync(productId);
                if (product != null)
                {
                    product.StockQuantity = quantity;
                    product.LastUpdated = DateTime.UtcNow;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<ProductEntity>> GetFeaturedProductsAsync()
        {
            return await _products
                .Include(p => p.Category)
                .Include(p => p.Images.Where(i => i.IsPrimary))
                .Where(p => p.IsFeatured && !p.IsDeleted)
                .OrderByDescending(p => p.CreatedAt)
                .Take(10)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductEntity>> GetNewArrivalsAsync()
        {
            return await _products
                .Include(p => p.Category)
                .Include(p => p.Images.Where(i => i.IsPrimary))
                .Where(p => !p.IsDeleted)
                .OrderByDescending(p => p.CreatedAt)
                .Take(10)
                .ToListAsync();
        }

        public async Task<decimal> GetLowestPriceAsync(int productId)
        {
            var product = await _products.FindAsync(productId);
            return product?.Price ?? 0;
        }

        public async Task<decimal> GetHighestPriceAsync(int productId)
        {
            var product = await _products.FindAsync(productId);
            return product?.Price ?? 0;
        }
    }
}
