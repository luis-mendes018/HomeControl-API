using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories;

public class UsuarioRepository :Repository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(AppDataContext context) 
        : base(context)
    {
        
    }

    public async Task<PagedResult<Usuario>> BuscarPorNomeUsuarioAsync(string nome, int pageNumber, int pageSize)
    {
        var query = _context.Usuarios
             .AsNoTracking()
             .Where(u =>
                 EF.Functions.Like(
                     EF.Functions.Collate(u.Nome, "utf8mb4_general_ci"),
                     $"%{nome}%"
                 ));

        var totalRecords = await query.CountAsync();

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Usuario>(
            items,
            pageNumber,
            pageSize,
            totalRecords
        );
    }
}
