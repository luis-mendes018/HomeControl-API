namespace Application.DTOs.Usuario;

/// <summary>
/// DTO de resposta que representa um usuário retornado pela API.
/// </summary>
public class UsuarioResponseDto
{
    /// <summary>
    /// Identificador único do usuário.
    /// </summary>
    /// <example>f2c1a6b3-8e4d-4c9a-9b7e-1a2b3c4d5e6f</example>
    public Guid Id { get; set; }

    /// <summary>
    /// Nome do usuário.
    /// </summary>
    /// <example>Maria Oliveira</example>
    public string Nome { get; set; }

    /// <summary>
    /// Idade do usuário.
    /// </summary>
    /// <example>28</example>
    public int Idade { get; set; }
}
