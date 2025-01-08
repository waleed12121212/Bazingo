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
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShoppingCartItem>> GetCartItemsByUserIdAsync(string userId)
        {
            return await _context.ShoppingCartItems.Where(c => c.BuyerID == userId).ToListAsync();
        }

        public async Task AddCartItemAsync(ShoppingCartItem cartItem)
        {
            await _context.ShoppingCartItems.AddAsync(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCartItemQuantityAsync(int cartItemId , int quantity)
        {
            var cartItem = await _context.ShoppingCartItems.FindAsync(cartItemId);
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                _context.ShoppingCartItems.Update(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveCartItemAsync(int cartItemId)
        {
            var cartItem = await _context.ShoppingCartItems.FindAsync(cartItemId);
            if (cartItem != null)
            {
                _context.ShoppingCartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ClearCartAsync(string userId)
        {
            var cartItems = await _context.ShoppingCartItems.Where(c => c.BuyerID == userId).ToListAsync();
            _context.ShoppingCartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
        }
    }
}
