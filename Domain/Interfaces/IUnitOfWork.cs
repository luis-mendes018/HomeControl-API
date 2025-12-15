namespace Domain.Interfaces;

public interface IUnitOfWork
{
    ICategoriaRepository CategoriaRepository { get; }
    IUsuarioRepository UsuarioRepository { get; }
    Task CommitAsync();
}
