using Domain.Common;
using Domain.Entities;

namespace Domain.Interfaces;

public interface ICategoriaRepository : IRepository<Categoria>
{
    Task<PagedResult<Categoria>> BuscarPorNomeAsync(
        string descricao,
        int pageNumber,
        int pageSize);

}
