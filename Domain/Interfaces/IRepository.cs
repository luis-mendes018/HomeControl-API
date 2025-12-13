using Domain.Common;

namespace Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(Guid id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);

    Task<PagedResult<T>> GetPagedAsync(int pageNumber, int pageSize);
}
