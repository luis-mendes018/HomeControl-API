namespace Application.DTOs.Usuario;

/// <summary>
/// DTO utilizado para criação de um novo usuário.
/// </summary>
public class UsuarioCreateDto
{
    /// <summary>
    /// Nome completo do usuário.
    /// </summary>
    /// <example>Maria Oliveira</example>
    public string Nome { get; set; }

    /// <summary>
    /// Idade do usuário.
    /// </summary>
    /// <example>28</example>
    public int Idade { get; set; }
}
