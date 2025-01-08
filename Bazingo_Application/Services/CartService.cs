using Bazingo_Core.Interfaces;
using Bazingo_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Application.Services
{
    public class CartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<IEnumerable<ShoppingCartItem>> GetCartItemsByUserIdAsync(string userId)
        {
            return await _cartRepository.GetCartItemsByUserIdAsync(userId);
        }

        public async Task AddCartItemAsync(ShoppingCartItem cartItem)
        {
            await _cartRepository.AddCartItemAsync(cartItem);
        }

        public async Task UpdateCartItemQuantityAsync(int cartItemId , int quantity)
        {
            await _cartRepository.UpdateCartItemQuantityAsync(cartItemId , quantity);
        }

        public async Task RemoveCartItemAsync(int cartItemId)
        {
            await _cartRepository.RemoveCartItemAsync(cartItemId);
        }

        public async Task ClearCartAsync(string userId)
        {
            await _cartRepository.ClearCartAsync(userId);
        }
    }
}
