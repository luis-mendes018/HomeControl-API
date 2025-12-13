using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;

using Infrastructure.Context;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CategoriaRepository
    : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDataContext context)
        : base(context)
    {
    }

    public async Task<PagedResult<Categoria>> BuscarPorNomeAsync(
    string descricao,
    int pageNumber,
    int pageSize)
    {
        var query = _context.Categorias
            .AsNoTracking()
            .Where(c =>
                EF.Functions.Like(
                    EF.Functions.Collate(c.Descricao, "utf8mb4_general_ci"),
                    $"%{descricao}%"));

        var totalRecords = await query.CountAsync();

        var items = await query
            .OrderBy(c => c.Descricao)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Categoria>(
            items,
            pageNumber,
            pageSize,
            totalRecords);
    }


}
