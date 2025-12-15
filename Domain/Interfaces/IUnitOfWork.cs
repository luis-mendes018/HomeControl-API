namespace Domain.Interfaces;

public interface IUnitOfWork
{
    ICategoriaRepository CategoriaRepository { get; }
    IUsuarioRepository UsuarioRepository { get; }
    ITransacaoRepository TransacaoRepository { get; }
    Task CommitAsync();
}
