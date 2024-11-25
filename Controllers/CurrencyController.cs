using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinancesApp.Models;
using FinancesApp.Services;

namespace FinancesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        // GET: api/Currency
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Currency>>> GetCurrencies()
        {
            var currencies = await _currencyService.GetCurrenciesAsync();
            return Ok(currencies);
        }

        // GET: api/Currency/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Currency>> GetCurrency(int id)
        {
            var currency = await _currencyService.GetCurrencyAsync(id);

            if (currency == null)
            {
                return NotFound();
            }

            return Ok(currency);
        }

        // PUT: api/Currency/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurrency(int id, Currency currency)
        {
            var currencyFromDb = await _currencyService.GetCurrencyAsync(id);

            if(currencyFromDb == null)
            {
                return NotFound("Currency not found");
            }

            var currencyWithSameName = await _currencyService.GetCurrencyAsync(currency.Name);

            if(currencyWithSameName != null && currencyWithSameName.Id != id)
            {
                return BadRequest("Currency with such name already exists");
            }
            var result = await _currencyService.UpdateCurrencyAsync(currency);
            

            return Ok(result);
        }

        // POST: api/Currency
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Currency>> PostCurrency(Currency currency)
        {
            var currencyWithSameName = await _currencyService.GetCurrencyAsync(currency.Name);

            if(currencyWithSameName != null)
            {
                return BadRequest("Currency with such name already exists");
            }
            var result = await _currencyService.CreateCurrencyAsync(currency);
            return Ok(result);
        }

        // DELETE: api/Currency/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurrency(int id)
        {
            var currency = await _currencyService.GetCurrencyAsync(id);

            if (currency == null)
            {
                return NotFound();
            }
            await _currencyService.DeleteCurrencyAsync(currency);
            return NoContent();
        }
    }
}
