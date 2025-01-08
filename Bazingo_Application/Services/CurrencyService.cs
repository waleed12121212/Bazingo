using Bazingo_Core.Interfaces;
using Bazingo_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Application.Services
{
    public class CurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyService(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public async Task<List<Currency>> GetAllCurrenciesAsync( )
        {
            return await _currencyRepository.GetAllAsync();
        }

        public async Task<Currency> GetCurrencyByIdAsync(int id)
        {
            return await _currencyRepository.GetByIdAsync(id);
        }

        public async Task UpdateCurrencyAsync(Currency currency)
        {
            await _currencyRepository.UpdateAsync(currency);
        }
    }
}
