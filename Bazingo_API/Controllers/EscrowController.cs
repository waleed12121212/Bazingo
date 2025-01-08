using Bazingo_Application.DTOs.Ecrows;
using Bazingo_Core.Models;
using Bazingo_Core.DomainLogic;
using Microsoft.AspNetCore.Mvc;

namespace Bazingo_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EscrowController : ControllerBase
    {
        private readonly EscrowManager _escrowManager;

        public EscrowController(EscrowManager escrowManager)
        {
            _escrowManager = escrowManager;
        }

        // Create Escrow
        [HttpPost]
        public async Task<IActionResult> CreateEscrow([FromBody] EscrowCreateDTO escrowCreateDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var escrow = new Escrow
            {
                OrderID = escrowCreateDTO.OrderID ,
                Amount = escrowCreateDTO.Amount ,
                Status = Enum.Parse<EscrowStatus>(escrowCreateDTO.Status , true) ,
                CreatedAt = DateTime.UtcNow
            };

            await _escrowManager.CreateEscrowAsync(escrow);
            return Ok(new { message = "Escrow created successfully." });
        }

        // Get Escrow by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEscrowById(int id)
        {
            var escrow = await _escrowManager.GetEscrowByIdAsync(id);
            if (escrow == null) return NotFound(new { message = "Escrow not found." });

            var escrowDTO = new EscrowDTO
            {
                EscrowID = escrow.EscrowID ,
                OrderID = escrow.OrderID ,
                Amount = escrow.Amount ,
                Status = escrow.Status.ToString() ,
                CreatedAt = escrow.CreatedAt
            };

            return Ok(escrowDTO);
        }

        // Update Escrow Status
        [HttpPut("status/{id}")]
        public async Task<IActionResult> UpdateEscrowStatus(int id , [FromBody] EscrowUpdateDTO escrowUpdateDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _escrowManager.UpdateEscrowStatusAsync(id , Enum.Parse<EscrowStatus>(escrowUpdateDTO.Status , true));
                return Ok(new { message = "Escrow status updated successfully." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Escrow not found." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Get All Escrows
        [HttpGet]
        public async Task<IActionResult> GetAllEscrows( )
        {
            var escrows = await _escrowManager.GetAllEscrowsAsync();
            var escrowDTOs = escrows.Select(e => new EscrowDTO
            {
                EscrowID = e.EscrowID ,
                OrderID = e.OrderID ,
                Amount = e.Amount ,
                Status = e.Status.ToString() ,
                CreatedAt = e.CreatedAt
            });

            return Ok(escrowDTOs);
        }
    }
}
