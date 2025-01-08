using Bazingo_Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bazingo_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly CurrencyService _currencyService;

        public CurrencyController(CurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCurrencies( )
        {
            var currencies = await _currencyService.GetAllCurrenciesAsync();
            return Ok(currencies);
        }

        [HttpPut("{id}/update-rate")]
        public async Task<IActionResult> UpdateExchangeRate(int id , [FromBody] decimal newRate)
        {
            var currency = await _currencyService.GetCurrencyByIdAsync(id);
            if (currency == null) return NotFound();

            currency.ExchangeRate = newRate;
            await _currencyService.UpdateCurrencyAsync(currency);

            return Ok(new { message = "Exchange rate updated successfully." });
        }
    }
}
