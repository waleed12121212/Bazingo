using Bazingo_Core.Interfaces;
using Bazingo_Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Bazingo_Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction _transaction;
        private bool _disposed;

        private IProductRepository _products;
        private ICategoryRepository _categories;
        private IOrderRepository _orders;
        private ICartRepository _carts;
        private IWishlistRepository _wishlists;
        private IProductReviewRepository _productReviews;
        private IComplaintRepository _complaints;
        private ICurrencyRepository _currencies;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IProductRepository Products => _products ??= new ProductRepository(_context);
        public ICategoryRepository Categories => _categories ??= new CategoryRepository(_context);
        public IOrderRepository Orders => _orders ??= new OrderRepository(_context);
        public ICartRepository Carts => _carts ??= new CartRepository(_context);
        public IWishlistRepository Wishlists => _wishlists ??= new WishlistRepository(_context);
        public IProductReviewRepository ProductReviews => _productReviews ??= new ProductReviewRepository(_context);
        public IComplaintRepository Complaints => _complaints ??= new ComplaintRepository(_context);
        public ICurrencyRepository Currencies => _currencies ??= new CurrencyRepository(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                }
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync()
        {
            try
            {
                if (_transaction != null)
                {
                    await _transaction.RollbackAsync();
                }
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
