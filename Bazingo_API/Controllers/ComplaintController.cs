using Bazingo_Application.Services;
using Bazingo_Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bazingo_Application.DTOs.Complaints;

namespace Bazingo_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComplaintController : ControllerBase
    {
        private readonly ComplaintService _complaintService;

        public ComplaintController(ComplaintService complaintService)
        {
            _complaintService = complaintService;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitComplaint([FromBody] ComplaintCreateDTO complaintCreateDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var complaint = new Complaint
            {
                UserID = complaintCreateDTO.UserID ,
                OrderID = complaintCreateDTO.OrderID ,
                Description = complaintCreateDTO.Description ,
                Status = Enum.Parse<ComplaintStatus>("Open" , true)
            };

            await _complaintService.AddComplaintAsync(complaint);
            return Ok(new { message = "Complaint submitted successfully." });
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserComplaints(string userId)
        {
            var complaints = await _complaintService.GetComplaintsByUserIdAsync(userId);
            return Ok(complaints);
        }

        [HttpPut("status/{id}")]
        public async Task<IActionResult> UpdateComplaintStatus(int id , [FromBody] string status)
        {
            await _complaintService.UpdateComplaintStatusAsync(id , status);
            return Ok(new { message = "Complaint status updated successfully." });
        }
    }
}
