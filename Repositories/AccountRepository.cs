using FinancesApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinancesApp.Repositories;

public class AccountRepository : Repository<Account>
{
    public AccountRepository(AppDbContext context) : base(context) { }

    public override async Task<Account?> CreateAsync(Account newAccount)
    {
        var currency = await _context.Currencies.FirstOrDefaultAsync(x => x.Id == newAccount.CurrencyId);

        if (currency == null)
        {
            return null;
        }

        var existingAccount = await base.GetAsync(x=> x.Name == newAccount.Name);

        if (existingAccount != null)
        {
            return null;
        }

        newAccount.Currency = currency;
        return await base.CreateAsync(newAccount);
    }
}