using Bazingo_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Core.Interfaces
{
    public interface IPaymentRepository
    {
        Task<Payment> GetPaymentByIdAsync(int paymentId);
        Task<IEnumerable<Payment>> GetAllPaymentsAsync( );
        Task AddPaymentAsync(Payment payment);
        Task UpdatePaymentStatusAsync(int paymentId , string status);
    }
}
