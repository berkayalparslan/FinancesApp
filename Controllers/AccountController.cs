using Microsoft.AspNetCore.Mvc;
using FinancesApp.Models;
using FinancesApp.Services;

namespace FinancesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ICurrencyService _currencyService;

        public AccountController(IAccountService accountService, ICurrencyService currencyService)
        {
            _accountService = accountService;
            _currencyService = currencyService;
        }

        // GET: api/Account
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            var accounts = await _accountService.GetAccountsAsync();
            return Ok(accounts);
        }

        // GET: api/Account/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(int id)
        {
            var account = await _accountService.GetAccountAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        // PUT: api/Account/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, Account account)
        {
            if (id != account.Id)
            {
                return BadRequest();
            }
            var accountFromDb = await _accountService.GetAccountAsync(id);

            if (accountFromDb == null)
            {
                return NotFound("Account not found");
            }
            var accountWithSameName = await _accountService.GetAccountAsync(account.Name);

            if (accountWithSameName != null && accountWithSameName.Id != id)
            {
                return BadRequest("Account with such name already exists");
            }
            var accountCurrency = await _currencyService.GetCurrencyAsync(account.CurrencyId);

            if (accountCurrency == null)
            {
                return BadRequest("Currency not found");
            }
            var result = await _accountService.UpdateAccountAsync(account);

            return Ok(result);
        }

        // POST: api/Account
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
            var accountWithSameName = await _accountService.GetAccountAsync(account.Name);

            if (accountWithSameName != null)
            {
                return BadRequest("Account with such name already exists");
            }
            var currency = await _currencyService.GetCurrencyAsync(account.CurrencyId);

            if (currency == null)
            {
                return BadRequest("Currency not found");
            }

            var result = _accountService.CreateAccountAsync(account);
            return Ok(result);
        }

        // DELETE: api/Account/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var account = await _accountService.GetAccountAsync(id);

            if (account == null)
            {
                return NotFound("Account not found");
            }
            await _accountService.DeleteAccountAsync(account);
            return NoContent();
        }
    }
}
