using Bazingo_Core.Interfaces;
using Bazingo_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Core.DomainLogic
{
    public class PaymentProcessor
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentProcessor(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task ProcessPaymentAsync(Payment payment)
        {
            if (payment == null)
            {
                throw new ArgumentNullException(nameof(payment));
            }

            payment.Status = PaymentStatus.Processing;
            await _paymentRepository.AddPaymentAsync(payment);

            // Simulate payment gateway logic
            payment.Status = PaymentStatus.Completed;
            await _paymentRepository.UpdatePaymentStatusAsync(payment.PaymentID , payment.Status.ToString());
        }
    }
}
