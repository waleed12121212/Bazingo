using Bazingo_Core.Entities;
using Bazingo_Core.Interfaces;
using Bazingo_Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Bazingo_Core.Entities.Product;

namespace Bazingo_Infrastructure.Repositories
{
    public class ReviewRepository : BaseRepository<ProductReviewEntity>, IProductReviewRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<ProductReviewEntity> _dbSet;

        public ReviewRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
            _dbSet = context.Set<ProductReviewEntity>();
        }

        public new async Task<ProductReviewEntity> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(r => r.User)
                .Include(r => r.Product)
                .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
        }

        public new async Task<IEnumerable<ProductReviewEntity>> GetAllAsync()
        {
            return await _dbSet
                .Include(r => r.User)
                .Include(r => r.Product)
                .Where(r => !r.IsDeleted)
                .OrderByDescending(r => r.ReviewDate)
                .ToListAsync();
        }

        public new async Task<ProductReviewEntity> AddAsync(ProductReviewEntity review)
        {
            review.ReviewDate = DateTime.UtcNow;
            review.CreatedAt = DateTime.UtcNow;
            await _dbSet.AddAsync(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<bool> UpdateAsync(ProductReviewEntity review)
        {
            try
            {
                review.LastUpdated = DateTime.UtcNow;
                _context.Entry(review).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var review = await GetByIdAsync(id);
            if (review != null)
            {
                review.IsDeleted = true;
                review.LastUpdated = DateTime.UtcNow;
                _context.Entry(review).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<ProductReviewEntity>> GetByProductIdAsync(int productId)
        {
            return await _dbSet
                .Include(r => r.User)
                .Include(r => r.Product)
                .Where(r => r.ProductId == productId && !r.IsDeleted)
                .OrderByDescending(r => r.ReviewDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductReviewEntity>> GetByUserIdAsync(string userId)
        {
            return await _dbSet
                .Include(r => r.User)
                .Include(r => r.Product)
                .Where(r => r.UserId == userId && !r.IsDeleted)
                .OrderByDescending(r => r.ReviewDate)
                .ToListAsync();
        }

        public async Task<double> GetAverageRatingAsync(int productId)
        {
            return await _dbSet
                .Where(r => r.ProductId == productId && !r.IsDeleted)
                .AverageAsync(r => r.Rating);
        }

        public async Task<int> GetReviewCountAsync(int productId)
        {
            return await _dbSet
                .CountAsync(r => r.ProductId == productId && !r.IsDeleted);
        }

        public async Task<bool> HasUserReviewedAsync(int productId, string userId)
        {
            return await _dbSet
                .AnyAsync(r => r.ProductId == productId && r.UserId == userId && !r.IsDeleted);
        }

        public async Task<IEnumerable<ProductReviewEntity>> GetRecentReviewsAsync(int count)
        {
            return await _dbSet
                .Include(r => r.User)
                .Include(r => r.Product)
                .Where(r => !r.IsDeleted)
                .OrderByDescending(r => r.ReviewDate)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductReviewEntity>> GetTopRatedReviewsAsync(int count)
        {
            return await _dbSet
                .Include(r => r.User)
                .Include(r => r.Product)
                .Where(r => !r.IsDeleted)
                .OrderByDescending(r => r.Rating)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductReviewEntity>> GetVerifiedReviewsAsync(int productId)
        {
            return await _dbSet
                .Include(r => r.User)
                .Include(r => r.Product)
                .Where(r => r.ProductId == productId && r.IsVerifiedPurchase && !r.IsDeleted)
                .OrderByDescending(r => r.ReviewDate)
                .ToListAsync();
        }

        public async Task<bool> MarkAsVerifiedPurchaseAsync(int reviewId)
        {
            var review = await GetByIdAsync(reviewId);
            if (review != null)
            {
                review.IsVerifiedPurchase = true;
                review.LastUpdated = DateTime.UtcNow;
                return await UpdateAsync(review);
            }
            return false;
        }
    }
}
