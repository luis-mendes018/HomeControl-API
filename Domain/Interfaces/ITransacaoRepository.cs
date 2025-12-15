using Domain.Common;
using Domain.Entities;

namespace Domain.Interfaces;

public interface ITransacaoRepository : IRepository<Transacao>
{
    Task<PagedResult<Transacao>> BuscarTransacaoPorNomeAsync(
        string descricao,
        int pageNumber,
        int pageSize);
}
