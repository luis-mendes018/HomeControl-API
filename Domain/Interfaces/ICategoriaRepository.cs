using Domain.Common;
using Domain.Entities;
using Domain.Models;

namespace Domain.Interfaces;

public interface ICategoriaRepository : IRepository<Categoria>
{
    Task<PagedResult<Categoria>> BuscarPorNomeAsync(
        string descricao,
        int pageNumber,
        int pageSize);
    
}
