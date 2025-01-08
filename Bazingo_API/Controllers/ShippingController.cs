using Bazingo_Application.DTOs.Carts;
using Bazingo_Application.Services;
using Bazingo_Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bazingo_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShippingController : ControllerBase
    {
        private readonly OrderService _orderService;

        public ShippingController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetShippingDetails(int orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null) return NotFound();

            return Ok(new ShippingDTO
            {
                Address = order.Shipping?.Address ,
                City = order.Shipping?.City ,
                Country = order.Shipping?.Country ,
                PostalCode = order.Shipping?.PostalCode ,
                TrackingNumber = order.Shipping?.TrackingNumber ,
                ShippingStatus = order.Shipping?.ShippingStatus.ToString() // تحويل ShippingStatus إلى سلسلة نصية
            });
        }

        [HttpPut("{orderId}/update-status")]
        public async Task<IActionResult> UpdateShippingStatus(int orderId , [FromBody] string status)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null || order.Shipping == null) return NotFound();

            try
            {
                order.Shipping.ShippingStatus = Enum.Parse<ShippingStatus>(status , true); // تحويل النص إلى تعداد
                await _orderService.UpdateOrderAsync(order);

                return Ok(new { message = "Shipping status updated successfully." });
            }
            catch (ArgumentException)
            {
                return BadRequest(new { message = "Invalid shipping status value." });
            }
        }
    }
}
