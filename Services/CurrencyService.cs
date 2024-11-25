using FinancesApp.Models;
using FinancesApp.Repositories;
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
    private readonly IRepository<Currency> _repository;

    public CurrencyService(IRepository<Currency> repository)
    {
        _repository = repository;
    }

    public async Task<Currency> CreateCurrencyAsync(Currency currency)
    {
        var currencyCreated = await _repository.CreateAsync(currency);
        return currencyCreated;
    }

    public async Task<bool> DeleteCurrencyAsync(Currency currency)
    {
        var deleted = await _repository.DeleteAsync(currency);
        return deleted;
    }

    public async Task<IEnumerable<Currency>> GetCurrenciesAsync()
    {
        return await _repository.GetAllAsync(x => true);
    }

    public async Task<Currency> GetCurrencyAsync(int id)
    {
        return await _repository.GetAsync(x => x.Id == id); // var currency = await _context.Currencies.FindAsync(id);
    }

    public async Task<Currency> GetCurrencyAsync(string name)
    {
        return await _repository.GetAsync(x => x.Name == name); 
    }

    public async Task<Currency> UpdateCurrencyAsync(Currency currency)
    {
        return await _repository.UpdateAsync(currency);
    }
}   