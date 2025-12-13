using Domain.Common;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDataContext _context;

    public Repository(AppDataContext context)
    {
        _context = context;
    }

    public async Task<T> GetByIdAsync(Guid id)
        => await _context.Set<T>().FindAsync(id);

    public async Task AddAsync(T entity)
        => await _context.Set<T>().AddAsync(entity);

    public Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<PagedResult<T>> GetPagedAsync(
        int pageNumber,
        int pageSize)
    {
        var query = _context.Set<T>().AsNoTracking();

        var totalRecords = await query.CountAsync();

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<T>(
            items,
            pageNumber,
            pageSize,
            totalRecords);
    }
}

