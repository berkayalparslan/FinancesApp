using FinancesApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinancesApp.Repositories;

public class CurrencyRepository : Repository<Currency>
{
    public CurrencyRepository(AppDbContext context) : base(context) { }

    public override async Task<Currency?> CreateAsync(Currency newCurrency)
    {
        var existingCurrencyWithSameName = await _context.Currencies.FirstOrDefaultAsync(x => x.Name == newCurrency.Name);

        if(existingCurrencyWithSameName != null)
        {
            return null;
        }

        return await base.CreateAsync(newCurrency);
    }
}