using Domain.Exceptions;

namespace Domain.Entities;

public class Usuario
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public int Idade { get; private set; }

    public ICollection<Transacao> Transacoes { get; private set; }

    public Usuario(string nome, int idade)
    {
        Id = Guid.NewGuid();
        DefinirIdade(idade);
        DefinirNome(nome);
    }

    //Construtor para EF Core
    private Usuario() 
    { 
        Transacoes = new List<Transacao>();
    }

    private void DefinirNome(string nome)
    {
        if(string.IsNullOrWhiteSpace(nome))
            throw new DomainException("O nome do usuário é obrigatório.");
        Nome = nome;
    }

    private void DefinirIdade(int idade)
    {
        if(idade <= 0)
            throw new DomainException("A idade do usuário deve ser maior que zero.");
        Idade = idade;
    }

    public void Atualizar(string nome, int idade)
    {
        DefinirNome(nome);
        DefinirIdade(idade);
    }
}
