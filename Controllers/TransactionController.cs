using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinancesApp.Models;
using FinancesApp.Services;

namespace FinancesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IAccountService _accountService;

        public TransactionController(ITransactionService transactionService, IAccountService accountService)
        {
            _transactionService = transactionService;
            _accountService = accountService;
        }

        // GET: api/Transaction
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            var transactions = await _transactionService.GetTransactionsAsync();
            return Ok(transactions);
        }

        // GET: api/Transaction/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(int id)
        {
            var transaction = await _transactionService.GetTransactionAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        // PUT: api/Transaction/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(int id, Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return BadRequest();
            }

            var accountFromDb = await _accountService.GetAccountAsync(transaction.AccountId);

            if (accountFromDb == null)
            {
                return NotFound("Account not found");
            }
            var result = await _transactionService.UpdateTransactionAsync(transaction);

            return Ok(result);
        }

        // POST: api/Transaction
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
        {
            var accountFromDb = await _accountService.GetAccountAsync(transaction.AccountId);

            if (accountFromDb == null)
            {
                return NotFound("Account not found");
            }
            var result = await _transactionService.CreateTransactionAsync(transaction);
            return Ok(result);
        }

        // DELETE: api/Transaction/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transaction = await _transactionService.GetTransactionAsync(id);

            if (transaction == null)
            {
                return NotFound("Transaction not found");
            }

            await _transactionService.DeleteTransactionAsync(transaction);
            return NoContent();
        }
    }
}
