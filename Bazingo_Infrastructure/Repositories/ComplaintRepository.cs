using Bazingo_Core.Entities;
using Bazingo_Core.Interfaces;
using Bazingo_Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bazingo_Core.Entities.Shopping;
using Bazingo_Core.Entities.Product;

namespace Bazingo_Infrastructure.Repositories
{
    public class ComplaintRepository : BaseRepository<Complaint>, IComplaintRepository
    {
        private readonly ApplicationDbContext _context;

        public ComplaintRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Complaint> GetComplaintByIdAsync(int id)
        {
            return await _dbSet
                .Include(c => c.User)
                .Include(c => c.Order)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        }

        public async Task<IReadOnlyList<Complaint>> GetComplaintsByOrderAsync(int orderId)
        {
            var complaints = await _dbSet
                .Include(c => c.User)
                .Include(c => c.Product)
                .Where(c => c.OrderId == orderId && !c.IsDeleted)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
            return complaints.AsReadOnly();
        }

        public async Task<IReadOnlyList<Complaint>> GetComplaintsByUserAsync(string userId)
        {
            var complaints = await _dbSet
                .Include(c => c.Order)
                .Include(c => c.Product)
                .Where(c => c.UserId == userId && !c.IsDeleted)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
            return complaints.AsReadOnly();
        }

        public async Task<IReadOnlyList<Complaint>> GetComplaintsByProductAsync(int productId)
        {
            var complaints = await _dbSet
                .Include(c => c.User)
                .Include(c => c.Order)
                .Where(c => c.ProductId == productId && !c.IsDeleted)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
            return complaints.AsReadOnly();
        }

        Task<IReadOnlyList<Complaint>> IComplaintRepository.GetComplaintsByStatusAsync(Bazingo_Core.Enums.ComplaintStatus status)
        {
            return _dbSet
                .Include(c => c.User)
                .Include(c => c.Order)
                .Include(c => c.Product)
                .Where(c => c.Status == (Bazingo_Core.Entities.ComplaintStatus)status && !c.IsDeleted)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync()
                .ContinueWith(t => (IReadOnlyList<Complaint>)t.Result.AsReadOnly());
        }

        public async Task<Complaint> AddComplaintAsync(Complaint complaint)
        {
            complaint.CreatedAt = DateTime.UtcNow;
            complaint.Status = (Bazingo_Core.Entities.ComplaintStatus)Bazingo_Core.Enums.ComplaintStatus.Pending;
            await AddAsync(complaint);
            await _context.SaveChangesAsync();
            return complaint;
        }

        public async Task<IReadOnlyList<Complaint>> GetComplaintsByUserIdAsync(string userId)
        {
            var complaints = await _dbSet
                .Include(c => c.Order)
                .Include(c => c.Product)
                .Where(c => c.UserId == userId && !c.IsDeleted)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
            return complaints.AsReadOnly();
        }

        public async Task UpdateComplaintStatusAsync(int id , string status)
        {
            var complaint = await GetByIdAsync(id);
            if (complaint != null && Enum.TryParse<Bazingo_Core.Enums.ComplaintStatus>(status , true , out Bazingo_Core.Enums.ComplaintStatus complaintStatus))
            {
                complaint.Status = (Bazingo_Core.Entities.ComplaintStatus)complaintStatus;
                complaint.LastUpdated = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
    }
}
