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
    public class ComplaintRepository : IComplaintRepository
    {
        private readonly ApplicationDbContext _context;

        public ComplaintRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Complaint> GetComplaintByIdAsync(int complaintId)
        {
            return await _context.Complaints.FindAsync(complaintId);
        }

        public async Task<IEnumerable<Complaint>> GetAllComplaintsAsync( )
        {
            return await _context.Complaints.ToListAsync();
        }

        public async Task<IEnumerable<Complaint>> GetComplaintsByUserIdAsync(string userId)
        {
            return await _context.Complaints.Where(c => c.UserID == userId).ToListAsync();
        }

        public async Task AddComplaintAsync(Complaint complaint)
        {
            await _context.Complaints.AddAsync(complaint);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateComplaintStatusAsync(int complaintId , string status)
        {
            var complaint = await GetComplaintByIdAsync(complaintId);
            if (complaint != null)
            {
                if (Enum.TryParse(status , out ComplaintStatus complaintStatus))
                {
                    complaint.Status = complaintStatus;
                    _context.Complaints.Update(complaint);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    // التعامل مع الحالة عندما لا يمكن تحويل السلسلة إلى قيمة من نوع ComplaintStatus
                    throw new ArgumentException("Invalid status value.");
                }
            }
        }
    }
}
