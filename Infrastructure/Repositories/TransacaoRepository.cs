using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;

using Infrastructure.Context;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TransacaoRepository : Repository<Transacao>, ITransacaoRepository
{
    public TransacaoRepository(AppDataContext context) 
        : base(context)
    {
        
    }
    public async Task<PagedResult<Transacao>> BuscarTransacaoPorNomeOuCodigoAsync(
        string filtro,
        int pageNumber, 
        int pageSize)
    {
        var query = _context.Transacoes
        .AsNoTracking()
        .Include(t => t.Usuario)
        .Include(t => t.Categoria)
        .AsQueryable();

        if (int.TryParse(filtro, out var codigo))
        {
            query = query.Where(t => t.CodigoTransacao == codigo);
        }
        else
        {
            
            query = query.Where(t =>
                EF.Functions.Like(
                    EF.Functions.Collate(t.Descricao, "utf8mb4_general_ci"),
                    $"%{filtro}%"));
        }

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

    public async Task<IEnumerable<UsuarioTotais>> ObterTotaisPorUsuarioAsync()
    {
        return await _context.Usuarios
            .Select(u => new UsuarioTotais
            {
                UsuarioId = u.Id,
                Nome = u.Nome,
                TotalReceita = u.Transacoes
                    .Where(t => t.Tipo == TipoTransacao.Receita)
                    .Sum(t => t.Valor),
                TotalDespesa = u.Transacoes
                    .Where(t => t.Tipo == TipoTransacao.Despesa)
                    .Sum(t => t.Valor)
            })
            .ToListAsync();
    }


    public async Task<IEnumerable<CategoriaTotais>> ObterTotaisPorCategoriaAsync()
    {
        return await _context.Transacoes
            .GroupBy(t => new { t.CategoriaId, t.Categoria.Descricao })
            .Select(g => new CategoriaTotais
            {
                CategoriaId = g.Key.CategoriaId,
                Nome = g.Key.Descricao,
                TotalReceita = g
                    .Where(t => t.Tipo == TipoTransacao.Receita)
                    .Sum(t => t.Valor),
                TotalDespesa = g
                    .Where(t => t.Tipo == TipoTransacao.Despesa)
                    .Sum(t => t.Valor)
            })
            .ToListAsync();
    }


}
