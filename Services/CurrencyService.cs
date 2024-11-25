using FinancesApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancesApp.Services;

public interface ICurrencyService
{
    Task<IEnumerable<Currency>> GetCurrenciesAsync();
    Task<Currency> GetCurrencyAsync(int id);
    Task<Currency> GetCurrencyAsync(string name);
    Task<Currency> CreateCurrencyAsync(Currency currency);
    Task<Currency> UpdateCurrencyAsync(Currency currency);
    Task<bool> DeleteCurrencyAsync(Currency currency);
}

public class CurrencyService : ICurrencyService
{
    private readonly AppDbContext _context;

    public CurrencyService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Currency> CreateCurrencyAsync(Currency currency)
    {
        var currencyResult =  _context.Currencies.Add(currency);
        await _context.SaveChangesAsync();
        return currencyResult.Entity;
    }

    public async Task<bool> DeleteCurrencyAsync(Currency currency)
    {
        _context.Currencies.Remove(currency);
         var result = await _context.SaveChangesAsync();
         return result > 0;
    }

    public async Task<IEnumerable<Currency>> GetCurrenciesAsync()
    {
        return await _context.Currencies.ToListAsync();
    }

    public async Task<Currency> GetCurrencyAsync(int id)
    {
        return await _context.Currencies.FirstOrDefaultAsync(x => x.Id == id); // var currency = await _context.Currencies.FindAsync(id);
    }

    public async Task<Currency> GetCurrencyAsync(string name)
    {
        return await _context.Currencies.FirstOrDefaultAsync(x => x.Name == name); 
    }

    public async Task<Currency> UpdateCurrencyAsync(Currency currency)
    {
        await _context.Entry(currency).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return currency;
    }
}   