using System.Linq.Expressions;

namespace FinancesApp.Repositories;

public interface IRepository<T> where T : class
{
    Task<T> GetAsync(Expression<Func<T, bool>> expression);
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(T entity);
}