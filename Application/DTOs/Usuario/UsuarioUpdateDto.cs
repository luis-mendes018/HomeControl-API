namespace Application.DTOs.Usuario;

/// <summary>
/// DTO utilizado para atualização dos dados de um usuário existente.
/// </summary>
public class UsuarioUpdateDto
{
    /// <summary>
    /// Novo nome do usuário.
    /// </summary>
    /// <example>Maria Oliveira</example>
    public string Nome { get; set; }

    /// <summary>
    /// Nova idade do usuário.
    /// </summary>
    /// <example>29</example>
    public int Idade { get; set; }
}
