using Bazingo_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Core.Interfaces
{
    public interface IComplaintRepository
    {
        Task<Complaint> GetComplaintByIdAsync(int complaintId);
        Task<IEnumerable<Complaint>> GetAllComplaintsAsync( );
        Task<IEnumerable<Complaint>> GetComplaintsByUserIdAsync(string userId);
        Task AddComplaintAsync(Complaint complaint);
        Task UpdateComplaintStatusAsync(int complaintId , string status);
    }

}
