using Domain.Enums;

namespace Application.DTOs.Categoria;

/// <summary>
/// DTO utilizado para atualização de uma categoria existente.
/// </summary>
public class CategoriaUpdateDto
{
    /// <summary>
    /// Nova descrição ou nome da categoria.
    /// </summary>
    /// <example>Lanches</example>
    public string Descricao { get; set; }

}
