using Bazingo_Core.Entities.Shopping;
using Bazingo_Core.Enums;
using Bazingo_Core.Interfaces;
using Bazingo_Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bazingo_Core.Specifications;

namespace Bazingo_Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<OrderEntity>, IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<OrderEntity> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Orders
                    .Include(o => o.User)
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                    .Include(o => o.StatusHistory)
                    .FirstOrDefaultAsync(o => o.Id == id && !o.IsDeleted);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting order by id: {ex.Message}");
            }
        }

        public async Task<OrderEntity> GetByIdWithItemsAsync(int id)
        {
            try
            {
                return await _context.Orders
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                    .FirstOrDefaultAsync(o => o.Id == id && !o.IsDeleted);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting order with items: {ex.Message}");
            }
        }

        public async Task<OrderEntity> GetByIdWithItemsAndStatusHistoryAsync(int id)
        {
            try
            {
                return await _context.Orders
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                    .Include(o => o.StatusHistory)
                    .FirstOrDefaultAsync(o => o.Id == id && !o.IsDeleted);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting order with items and status history: {ex.Message}");
            }
        }

        public async Task<IReadOnlyList<OrderEntity>> GetAllAsync()
        {
            try
            {
                return await _context.Orders
                    .Include(o => o.User)
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                    .Where(o => !o.IsDeleted)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting all orders: {ex.Message}");
            }
        }

        public async Task<IReadOnlyList<OrderEntity>> GetAllWithItemsAsync()
        {
            try
            {
                return await _context.Orders
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                    .Where(o => !o.IsDeleted)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting all orders with items: {ex.Message}");
            }
        }

        public async Task<IReadOnlyList<OrderEntity>> GetByUserIdAsync(string userId)
        {
            try
            {
                return await _context.Orders
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                    .Where(o => o.UserId == userId && !o.IsDeleted)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting orders by user id: {ex.Message}");
            }
        }

        public async Task<IReadOnlyList<OrderEntity>> GetByUserIdWithItemsAsync(string userId)
        {
            try
            {
                return await _context.Orders
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                    .Where(o => o.UserId == userId && !o.IsDeleted)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting orders with items by user id: {ex.Message}");
            }
        }

        public async Task<IReadOnlyList<OrderEntity>> GetUserOrdersAsync(string userId, int page = 1, int pageSize = 10)
        {
            try
            {
                return await _context.Orders
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                    .Include(o => o.StatusHistory)
                    .Where(o => o.UserId == userId && !o.IsDeleted)
                    .OrderByDescending(o => o.OrderDate)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting paginated orders by user id: {ex.Message}");
            }
        }

        public async Task<IReadOnlyList<OrderEntity>> GetOrdersByStatusAsync(OrderStatus status, int page = 1, int pageSize = 10)
        {
            try
            {
                return await _context.Orders
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                    .Include(o => o.StatusHistory)
                    .Where(o => o.Status == status && !o.IsDeleted)
                    .OrderByDescending(o => o.OrderDate)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting paginated orders by status: {ex.Message}");
            }
        }

        public async Task<OrderEntity> UpdateOrderStatusAsync(int orderId, OrderStatus newStatus, string comment = null)
        {
            try
            {
                var order = await GetByIdWithItemsAndStatusHistoryAsync(orderId);
                if (order == null)
                    throw new Exception($"Order with ID {orderId} not found");

                order.Status = newStatus;
                
                if (comment != null)
                {
                    order.StatusHistory.Add(new OrderStatusHistoryEntity
                    {
                        OrderId = orderId,
                        Status = newStatus,
                        Comment = comment,
                        ChangedDate = DateTime.UtcNow
                    });
                }

                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                return order;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating order status: {ex.Message}");
            }
        }

        public async Task<OrderEntity> UpdatePaymentStatusAsync(int orderId, PaymentStatus newStatus)
        {
            try
            {
                var order = await GetByIdWithItemsAndStatusHistoryAsync(orderId);
                if (order == null)
                    throw new Exception($"Order with ID {orderId} not found");

                order.PaymentStatus = newStatus;
                
                order.StatusHistory.Add(new OrderStatusHistoryEntity
                {
                    OrderId = orderId,
                    Status = order.Status,
                    Comment = $"Payment status updated to {newStatus}",
                    ChangedDate = DateTime.UtcNow
                });

                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                return order;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating payment status: {ex.Message}");
            }
        }

        public async Task<OrderEntity> AddAsync(OrderEntity entity)
        {
            try
            {
                await _context.Orders.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding order: {ex.Message}");
            }
        }

        public async Task<OrderEntity> UpdateAsync(OrderEntity entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating order: {ex.Message}");
            }
        }

        public async Task DeleteAsync(OrderEntity entity)
        {
            try
            {
                entity.IsDeleted = true;
                await UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting order: {ex.Message}");
            }
        }

        public async Task<OrderEntity> GetEntityWithSpec(ISpecification<OrderEntity> spec)
        {
            try
            {
                var query = ApplySpecification(spec);
                return await query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting order with specification: {ex.Message}");
            }
        }

        private IQueryable<OrderEntity> ApplySpecification(ISpecification<OrderEntity> spec)
        {
            return SpecificationEvaluator<OrderEntity>.GetQuery(_context.Orders.AsQueryable(), spec);
        }
    }
}
