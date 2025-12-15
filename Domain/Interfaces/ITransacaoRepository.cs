using Domain.Common;
using Domain.Entities;
using Domain.Models;

namespace Domain.Interfaces;

public interface ITransacaoRepository : IRepository<Transacao>
{
    Task<PagedResult<Transacao>> BuscarTransacaoPorNomeOuCodigoAsync(
        string filtro,
        int pageNumber,
        int pageSize);

    Task<IEnumerable<UsuarioTotais>> ObterTotaisPorUsuarioAsync();
    Task<IEnumerable<CategoriaTotais>> ObterTotaisPorCategoriaAsync();
}
