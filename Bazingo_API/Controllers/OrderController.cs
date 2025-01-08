using Bazingo_Application.Services;
using Bazingo_Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bazingo_Application.DTOs.Orders;

namespace Bazingo_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDTO orderCreateDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var order = new Order
            {
                BuyerID = orderCreateDTO.BuyerID ,
                Status = Enum.Parse<OrderStatus>("Pending" , true) // تحويل النص إلى enum
            };

            await _orderService.AddOrderAsync(order);
            return Ok(new { message = "Order created successfully." });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();

            return Ok(order);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserOrders(string userId)
        {
            var orders = await _orderService.GetAllOrdersAsync();
            var userOrders = orders.Where(o => o.BuyerID == userId);

            return Ok(userOrders);
        }
    }
}
