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
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Payment> GetPaymentByIdAsync(int paymentId)
        {
            return await _context.Payments.FindAsync(paymentId);
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync( )
        {
            return await _context.Payments.ToListAsync();
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePaymentStatusAsync(int paymentId , string status)
        {
            var payment = await GetPaymentByIdAsync(paymentId);
            if (payment != null)
            {
                if (Enum.TryParse(status , out PaymentStatus paymentStatus))
                {
                    payment.Status = paymentStatus;
                    _context.Payments.Update(payment);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    // التعامل مع الحالة عندما لا يمكن تحويل السلسلة إلى قيمة من نوع PaymentStatus
                    throw new ArgumentException("Invalid status value.");
                }
            }
        }
    }

}
