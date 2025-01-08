using Bazingo_Core.Interfaces;
using Bazingo_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Core.DomainLogic
{
    public class EscrowManager
    {
        private readonly IEscrowRepository _escrowRepository;

        public EscrowManager(IEscrowRepository escrowRepository)
        {
            _escrowRepository = escrowRepository;
        }

        // Create Escrow
        public async Task CreateEscrowAsync(Escrow escrow)
        {
            await _escrowRepository.AddEscrowAsync(escrow);
        }

        // Get Escrow by ID
        public async Task<Escrow> GetEscrowByIdAsync(int escrowId)
        {
            return await _escrowRepository.GetEscrowByIdAsync(escrowId)
                   ?? throw new KeyNotFoundException("Escrow not found.");
        }

        // Update Escrow Status
        public async Task UpdateEscrowStatusAsync(int escrowId , EscrowStatus status)
        {
            var escrow = await _escrowRepository.GetEscrowByIdAsync(escrowId);
            if (escrow == null)
            {
                throw new KeyNotFoundException("Escrow not found.");
            }

            escrow.Status = status;
            await _escrowRepository.UpdateEscrowAsync(escrow);
        }

        // Get All Escrows
        public async Task<IEnumerable<Escrow>> GetAllEscrowsAsync( )
        {
            return await _escrowRepository.GetAllEscrowsAsync();
        }
    }
}
