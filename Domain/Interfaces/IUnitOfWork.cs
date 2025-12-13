namespace Domain.Interfaces;

public interface IUnitOfWork
{
    ICategoriaRepository CategoriaRepository { get; }
    Task CommitAsync();
}
