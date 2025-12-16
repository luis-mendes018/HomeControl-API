using Domain.Enums;

namespace Application.DTOs.Transacao;

/// <summary>
/// DTO utilizado para criação de uma nova transação financeira.
/// </summary>
public class TransacaoCreateDto
{
    /// <summary>
    /// Valor da transação.
    /// </summary>
    /// <example>250.00</example>
    public decimal Valor { get; set; }

    /// <summary>
    /// Descrição da transação.
    /// </summary>
    /// <example>Compra de materiais de escritório</example>
    public string Descricao { get; set; }

    /// <summary>
    /// Identificador da categoria associada à transação.
    /// </summary>
    /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
    public Guid CategoriaId { get; set; }

    /// <summary>
    /// Tipo da transação, indicando se é uma receita ou despesa.
    /// </summary>
    /// <example>Despesa</example>
    public TipoTransacao Tipo { get; set; }

    /// <summary>
    /// Identificador do usuário responsável pela transação.
    /// </summary>
    /// <example>8b5d9e62-1b42-4f77-9a9f-4f6d2b8c1234</example>
    public Guid UsuarioId { get; set; }

}