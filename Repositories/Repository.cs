using FinancesApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinancesApp.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _context;
    public Repository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(expression);
    }
    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression)
    {
        return await _context.Set<T>().Where(expression).ToListAsync();
    }

    public async Task<T> CreateAsync(T entity)
    {
        var result = await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        var result = _context.Update(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        _context.Remove(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }
}