using Bazingo_Application.DTOs.Carts;
using Bazingo_Application.Services;
using Bazingo_Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bazingo_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] ShoppingCartItemCreateDTO cartItemDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var cartItem = new ShoppingCartItem
            {
                BuyerID = cartItemDTO.BuyerID ,
                ProductID = cartItemDTO.ProductID ,
                Quantity = cartItemDTO.Quantity
            };

            await _cartService.AddCartItemAsync(cartItem);
            return Ok(new { message = "Item added to cart successfully." });
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetCartItems(string userId)
        {
            var cartItems = await _cartService.GetCartItemsByUserIdAsync(userId);
            return Ok(cartItems);
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            await _cartService.RemoveCartItemAsync(id);
            return Ok(new { message = "Item removed from cart." });
        }

        [HttpDelete("clear/{userId}")]
        public async Task<IActionResult> ClearCart(string userId)
        {
            await _cartService.ClearCartAsync(userId);
            return Ok(new { message = "Cart cleared." });
        }
    }
}
