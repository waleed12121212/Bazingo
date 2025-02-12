using Bazingo_Core.Entities;
using Bazingo_Core.Interfaces;
using Bazingo_Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bazingo_Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Category>> GetMainCategoriesAsync()
        {
            return await _dbSet
                .Include(c => c.SubCategories)
                .Where(c => c.ParentCategoryId == null && !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetSubCategoriesAsync(int parentId)
        {
            return await _dbSet
                .Where(c => c.ParentCategoryId == parentId && !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<Category> GetCategoryWithProductsAsync(int id)
        {
            return await _dbSet
                .Include(c => c.Products.Where(p => !p.IsDeleted))
                    .ThenInclude(p => p.Seller)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> GetCategoryWithSubCategoriesAsync(int id)
        {
            return await _dbSet
                .Include(c => c.SubCategories.Where(sc => !sc.IsDeleted))
                .Include(c => c.ParentCategory)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public new async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _dbSet
                .Include(c => c.ParentCategory)
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Category category)
        {
            try
            {
                await base.UpdateAsync(category);
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
            var category = await GetByIdAsync(id);
            if (category == null)
                return false;

            category.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetProductCountAsync(int categoryId)
        {
            return await _context.Products
                .CountAsync(p => p.CategoryId == categoryId && !p.IsDeleted);
        }

        public async Task<bool> IsCategoryEmptyAsync(int categoryId)
        {
            var hasProducts = await _context.Products
                .AnyAsync(p => p.CategoryId == categoryId && !p.IsDeleted);

            var hasSubCategories = await _dbSet
                .AnyAsync(c => c.ParentCategoryId == categoryId && !c.IsDeleted);

            return !hasProducts && !hasSubCategories;
        }
    }
}
