using System.Text.Json.Serialization;

namespace Application.DTOs.TotalCategoria;

/// <summary>
/// DTO que representa os totais financeiros agrupados por categoria.
/// </summary>
public class TotaisPorCategoriaDto
{
    /// <summary>
    /// Identificador da categoria.
    /// Pode ser nulo quando os totais não estão associados a uma categoria específica.
    /// </summary>
    /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
    /// 
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Guid? CategoriaId { get; set; }

    /// <summary>
    /// Nome ou descrição da categoria.
    /// </summary>
    /// <example>Alimentação</example>
    public string Nome { get; set; }

    /// <summary>
    /// Valor total de receitas associadas à categoria.
    /// </summary>
    /// <example>1500.75</example>
    public decimal TotalReceita { get; set; }

    /// <summary>
    /// Valor total de despesas associadas à categoria.
    /// </summary>
    /// <example>820.40</example>
    public decimal TotalDespesa { get; set; }

    /// <summary>
    /// Saldo financeiro da categoria, calculado pela diferença entre
    /// o total de receitas e o total de despesas.
    /// </summary>
    /// <example>680.35</example>
    public decimal Saldo => TotalReceita - TotalDespesa;
}
