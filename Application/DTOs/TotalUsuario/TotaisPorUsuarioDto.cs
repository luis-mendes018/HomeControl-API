using System.Text.Json.Serialization;

namespace Application.DTOs.TotalUsuario;

/// <summary>
/// DTO que representa os totais financeiros agrupados por usuário.
/// </summary>
public class TotaisPorUsuarioDto
{
    /// <summary>
    /// Identificador do usuário.
    /// Pode ser nulo quando os totais não estão associados a um usuário específico.
    /// </summary>
    /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Guid? UsuarioId { get; set; }

    /// <summary>
    /// Nome do usuário.
    /// </summary>
    /// <example>João da Silva</example>
    public string Nome { get; set; }

    /// <summary>
    /// Valor total de receitas associadas ao usuário.
    /// </summary>
    /// <example>3200.00</example>
    public decimal TotalReceita { get; set; }

    /// <summary>
    /// Valor total de despesas associadas ao usuário.
    /// </summary>
    /// <example>1950.75</example>
    public decimal TotalDespesa { get; set; }

    /// <summary>
    /// Saldo financeiro do usuário, calculado pela diferença entre
    /// o total de receitas e o total de despesas.
    /// </summary>
    /// <example>1249.25</example>
    public decimal Saldo => TotalReceita - TotalDespesa;
}
