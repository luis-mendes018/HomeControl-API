using Domain.Enums;

namespace Application.DTOs.Categoria;

/// <summary>
/// DTO de resposta que representa uma categoria retornada pela API.
/// </summary>
public class CategoriaResponseDto
{
    /// <summary>
    /// Identificador único da categoria.
    /// </summary>
    /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
    public Guid Id { get; set; }

    /// <summary>
    /// Descrição ou nome da categoria.
    /// </summary>
    /// <example>Bebidas</example>
    /// 
    public string Descricao { get; set; }

    /// <summary>
    /// Finalidade da categoria, indicando seu tipo ou uso no sistema.
    /// </summary>
    /// <example>Produto</example>
    public FinalidadeCategoria Finalidade { get; set; }
}
