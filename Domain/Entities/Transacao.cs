using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities;

public class Transacao
{
    public Guid Id { get; private set; }
    public int CodigoTransacao { get; private set; }
    public decimal Valor { get; private set; }
    public string Descricao { get; private set; }
    public Guid CategoriaId { get; private set; }
    public Categoria Categoria { get; private set; }
    public TipoTransacao Tipo { get; private set; }

    public Guid UsuarioId { get; set; }
    public Usuario Usuario { get; set; }

    //Contrutor para criação de uma nova transação
    public Transacao(decimal valor, 
        string descricao, 
        Categoria categoria,
         TipoTransacao tipoTransacao,
         Usuario usuario)
    {
       Id = Guid.NewGuid();

        CodigoTransacao = GerarCodigoTransacao();

        DefinirValor(valor);
        DefinirDescricao(descricao);
        DefinirCategoria(categoria);
        SetTipoTransacao(tipoTransacao);
        DefinirUsuario(usuario);
    }

    //Contrutor para EF Core
    private Transacao() { }

    private void DefinirValor(decimal valor)
    {
        if(valor <= 0)
            throw new DomainException("O valor da transação deve ser maior que zero.");
        Valor = valor;

    }

    private void DefinirDescricao(string descricao)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new DomainException("A descrição é obrigatória.");
        Descricao = descricao;
    }


    private void DefinirCategoria(Categoria categoria)
    {
        if (categoria == null)
            throw new DomainException("A categoria é obrigatória.");
        Categoria = categoria;
        CategoriaId = categoria.Id;
    }

    private void SetTipoTransacao(TipoTransacao tipoTransacao)
    {
        if (!Enum.IsDefined(typeof(TipoTransacao), tipoTransacao))
            throw new DomainException("Tipo de transação inválida.");

        Tipo = tipoTransacao;
    }

    private void DefinirUsuario(Usuario usuario)
    {
        if (usuario == null)
            throw new DomainException("O usuário é obrigatório.");
        Usuario = usuario;
        UsuarioId = usuario.Id;
    }

    private static int GerarCodigoTransacao()
    {
        return Random.Shared.Next(1000000000, int.MaxValue);
    }
}
