using FinancesApp.Models;
using FinancesApp.Repositories;

namespace FinancesApp.Services;

public interface ITransactionService
{
    Task<Transaction> CreateTransactionAsync(Transaction transaction);
    Task<Transaction> UpdateTransactionAsync(Transaction transaction);
    Task<bool> DeleteTransactionAsync(Transaction transaction);
    Task<Transaction> GetTransactionAsync(int id);
    Task<Transaction> GetTransactionAsync(string transactionName);
    Task<IEnumerable<Transaction>> GetTransactionsAsync();
}

public class TransactionService : ITransactionService
{
    private readonly IRepository<Transaction> _repository;

    public TransactionService(IRepository<Transaction> repository)
    {
        _repository = repository;
    }

    public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
    {
        return await _repository.CreateAsync(transaction);
    }

    public async Task<Transaction> UpdateTransactionAsync(Transaction transaction)
    {
        return await _repository.UpdateAsync(transaction);
    }

    public async Task<bool> DeleteTransactionAsync(Transaction transaction)
    {
        return await _repository.DeleteAsync(transaction);
    }

    public async Task<Transaction> GetTransactionAsync(int id)
    {
        return await _repository.GetAsync(x => x.Id == id);
    }

    public async Task<Transaction> GetTransactionAsync(string transactionName)
    {
        return await _repository.GetAsync(x => x.Name == transactionName);
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsAsync()
    {
        return await _repository.GetAllAsync(x=>true);
    }
}