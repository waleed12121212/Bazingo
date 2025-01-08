using Bazingo_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Core.Interfaces
{
    public interface IEscrowRepository
    {
        Task AddEscrowAsync(Escrow escrow);
        Task<Escrow> GetEscrowByIdAsync(int escrowId);
        Task UpdateEscrowAsync(Escrow escrow);
        Task<IEnumerable<Escrow>> GetAllEscrowsAsync( );
    }
}
