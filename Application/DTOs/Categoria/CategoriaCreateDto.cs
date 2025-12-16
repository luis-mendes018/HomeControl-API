using Domain.Enums;

namespace Application.DTOs.Categoria;

/// <summary>
/// DTO utilizado para criação de uma nova categoria.
/// </summary>
public class CategoriaCreateDto
{
    /// <summary>
    /// Descrição da categoria.
    /// </summary>
    /// <example>Alimentação</example>
    public string Descricao { get; set; }

    /// <summary>
    /// Finalidade da categoria (Receita ou Despesa).
    /// </summary>
    /// <example>Despesa</example>
    public FinalidadeCategoria Finalidade { get; set; }
}
