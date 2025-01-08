using Bazingo_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Core.Interfaces
{
    public interface ICurrencyRepository
    {
        Task<List<Currency>> GetAllAsync( );
        Task<Currency> GetByIdAsync(int id);
        Task AddAsync(Currency currency);
        Task UpdateAsync(Currency currency);
        Task DeleteAsync(int id);
    }
}
