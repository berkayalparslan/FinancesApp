using FinancesApp.Models;
using FinancesApp.Repositories;

namespace FinancesApp.Services;

public interface IAccountService
{
    Task<IEnumerable<Account>> GetAccountsAsync();
    Task<Account> GetAccountAsync(int id);
    Task<Account> GetAccountAsync(string accountName);
    Task<Account> CreateAccountAsync(Account account);
    Task<Account> UpdateAccountAsync(Account account);
    Task<bool> DeleteAccountAsync(Account account);
}

public class AccountService : IAccountService
{
    private readonly IRepository<Account> _repository;

    public AccountService(IRepository<Account> repository)
    {
        _repository = repository;
    }

    public async Task<Account> CreateAccountAsync(Account account)
    {
        return await _repository.CreateAsync(account);
    }

    public async Task<bool> DeleteAccountAsync(Account account)
    {
        return await _repository.DeleteAsync(account);
    }

    public async Task<IEnumerable<Account>> GetAccountsAsync()
    {
        return await _repository.GetAllAsync(x => true);
    }

    public async Task<Account> GetAccountAsync(int id)
    {
        return await _repository.GetAsync(x => x.Id == id);
    }

    public async Task<Account> GetAccountAsync(string accountName)
    {
        return await _repository.GetAsync(x => x.Name == accountName);
    }

    public async Task<Account> UpdateAccountAsync(Account account)
    {
        return await _repository.UpdateAsync(account);
    }
}