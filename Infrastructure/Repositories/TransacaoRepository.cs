using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TransacaoRepository : Repository<Transacao>, ITransacaoRepository
{
    public TransacaoRepository(AppDataContext context) 
        : base(context)
    {
        
    }
    public async Task<PagedResult<Transacao>> BuscarTransacaoPorNomeAsync(
        string descricao, 
        int pageNumber, 
        int pageSize)
    {
        var query = _context.Transacoes
            .AsNoTracking().
            Where(t =>
                EF.Functions.Like(
                    EF.Functions.Collate(t.Descricao, "utf8mb4_general_ci"),
                    $"%{descricao}%"));

        var totalRecords = await query.CountAsync();

        var items = await query
            .OrderBy(c => c.Descricao)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Transacao>(
            items,
            pageNumber,
            pageSize,
            totalRecords);
    }
}
