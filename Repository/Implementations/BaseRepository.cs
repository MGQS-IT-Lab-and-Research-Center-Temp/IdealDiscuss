using IdealDiscuss.Context;
using IdealDiscuss.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IdealDiscuss.Repository.Implementations;

public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity, new()
{
    protected readonly IdealDiscussContext _context;

    protected BaseRepository(IdealDiscussContext context)
    {
        _context = context;
    }

    public async Task<T> CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);

        return entity;
    }

    public async Task<T> GetAsync(string id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
    {
        return await _context.Set<T>().SingleOrDefaultAsync(expression);
    }

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression)
    {
        return await _context.Set<T>().AnyAsync(expression);
    }

    public Task RemoveAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Deleted;
        return Task.CompletedTask;
    }

    public Task<T> UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;

        return Task.FromResult(entity);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null)
    {
        return await _context.Set<T>()
            .Where(expression)
            .ToListAsync();
    }

    public async Task<List<T>> GetAllByIdsAsync(List<string> ids)
    {
        return await _context.Set<T>()
            .Where(t => ids.Contains(t.Id))
            .ToListAsync();
    }

    public async Task<IReadOnlyList<T>> SelectAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> SelectAll(Expression<Func<T, bool>> expression = null)
    {
        return await _context.Set<T>().Where(expression).ToListAsync();
    }
}
