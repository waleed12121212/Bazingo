using Bazingo_Core.Interfaces;
using Bazingo_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Application.Services
{
    public class ComplaintService
    {
        private readonly IComplaintRepository _complaintRepository;

        public ComplaintService(IComplaintRepository complaintRepository)
        {
            _complaintRepository = complaintRepository;
        }

        public async Task<Complaint> GetComplaintByIdAsync(int complaintId)
        {
            return await _complaintRepository.GetComplaintByIdAsync(complaintId);
        }

        public async Task<IEnumerable<Complaint>> GetAllComplaintsAsync( )
        {
            return await _complaintRepository.GetAllComplaintsAsync();
        }

        public async Task<IEnumerable<Complaint>> GetComplaintsByUserIdAsync(string userId)
        {
            return await _complaintRepository.GetComplaintsByUserIdAsync(userId);
        }

        public async Task AddComplaintAsync(Complaint complaint)
        {
            await _complaintRepository.AddComplaintAsync(complaint);
        }

        public async Task UpdateComplaintStatusAsync(int complaintId , string status)
        {
            await _complaintRepository.UpdateComplaintStatusAsync(complaintId , status);
        }
    }
}
