using Domain.Common;
using Domain.Entities;

namespace Domain.Interfaces;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<PagedResult<Usuario>> BuscarPorNomeUsuarioAsync(
        string nome,
        int pageNumber,
        int pageSize);
}
