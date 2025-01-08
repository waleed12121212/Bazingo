using Bazingo_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Core.Interfaces
{
    public interface ICartRepository
    {
        Task<IEnumerable<ShoppingCartItem>> GetCartItemsByUserIdAsync(string userId);
        Task AddCartItemAsync(ShoppingCartItem cartItem);
        Task UpdateCartItemQuantityAsync(int cartItemId , int quantity);
        Task RemoveCartItemAsync(int cartItemId);
        Task ClearCartAsync(string userId);
    }
}
