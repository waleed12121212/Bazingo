using Bazingo_Application.DTOs.Payments;
using Bazingo_Core.DomainLogic;
using Bazingo_Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bazingo_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentProcessor _paymentProcessor;

        public PaymentController(PaymentProcessor paymentProcessor)
        {
            _paymentProcessor = paymentProcessor;
        }

        [HttpPost]
        public async Task<IActionResult> InitiatePayment([FromBody] PaymentCreateDTO paymentDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var payment = new Payment
            {
                OrderID = paymentDTO.OrderID ,
                PaymentAmount = paymentDTO.Amount ,
                PaymentMethod = Enum.Parse<PaymentMethod>(paymentDTO.Method , ignoreCase: true)
            };

            await _paymentProcessor.ProcessPaymentAsync(payment);
            return Ok(new { message = "Payment processed successfully." });
        }
    }
}
