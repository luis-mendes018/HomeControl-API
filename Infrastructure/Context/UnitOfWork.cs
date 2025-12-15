using Domain.Interfaces;

using Infrastructure.Repositories;

namespace Infrastructure.Context;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDataContext _context;
    private ICategoriaRepository _categoriaRepository;
    private IUsuarioRepository _usuarioRepository;
    private ITransacaoRepository _transacaoRepository;

    public UnitOfWork(AppDataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public ICategoriaRepository CategoriaRepository
        => _categoriaRepository ??= new CategoriaRepository(_context);

    public IUsuarioRepository UsuarioRepository
        => _usuarioRepository ??= new UsuarioRepository(_context);

    public ITransacaoRepository TransacaoRepository
        => _transacaoRepository ??= new TransacaoRepository(_context);

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}

