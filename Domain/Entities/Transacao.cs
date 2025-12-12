using Domain.Exceptions;

namespace Domain.Entities;

public class Transacao
{
    public Guid Id { get; private set; }
    public int CodigoTransacao { get; private set; }
    public decimal Valor { get; private set; }
    public DateTime Data { get; private set; }
    public bool EhAgendada { get; private set; }
    public string Descricao { get; private set; }
    public Guid CategoriaId { get; private set; }
    public Categoria Categoria { get; private set; }


    //Contrutor para criação de uma nova transação
    public Transacao(decimal valor, DateTime data, 
        string descricao, 
        Guid categoriaId, 
        Categoria categoria,
         bool ehAgendada,
         int codigoTransacao)
    {
       Id = Guid.NewGuid();
        DefinirValor(valor);
        DefinirDescricao(descricao);
        DefinirAgendamento(ehAgendada);
        DefinirData(data, EhAgendada);
        DefinirCategoria(categoria);
        CodigoTransacao = codigoTransacao;
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

    private void DefinirAgendamento(bool agendada)
    {
        EhAgendada = agendada;
    }

    private void DefinirData(DateTime data, bool ehAgendada)
    {
        if (ehAgendada)
        {
            if (data <= DateTime.Now)
                throw new DomainException("Transações agendadas devem ter data futura.");

            Data = data;
        }
        else
        {
            // Transação imediata
            Data = DateTime.Now;
        }
    }

    private void DefinirCategoria(Categoria categoria)
    {
        if (categoria == null)
            throw new DomainException("A categoria é obrigatória.");
        Categoria = categoria;
        CategoriaId = categoria.Id;
    }

}
