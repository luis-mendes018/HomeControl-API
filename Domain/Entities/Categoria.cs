using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities;

public class Categoria
{
    public Guid Id { get; private set; }
    public string Descricao { get; private set; }
    public FinalidadeCategoria Finalidade { get; private set; }

    public Categoria(string descricao, FinalidadeCategoria finalidade)
    {
        Id = Guid.NewGuid();
        SetDescricao(descricao);
        SetFinalidade(finalidade);
    }

    //Construtor para EF Core
    private Categoria() { }

    private void SetDescricao(string descricao)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new DomainException("A descrição da categoria é obrigatória.");

        if (descricao.Length > 200)
            throw new DomainException("A descrição da categoria deve ter no máximo 200 caracteres.");

        Descricao = descricao;
    }

    private void SetFinalidade(FinalidadeCategoria finalidade)
    {
        if (!Enum.IsDefined(typeof(FinalidadeCategoria), finalidade))
            throw new DomainException("Finalidade inválida para categoria.");

        Finalidade = finalidade;
    }

    public void Atualizar(string descricao, FinalidadeCategoria finalidade)
    {
        SetDescricao(descricao);
        SetFinalidade(finalidade);
    }

}
