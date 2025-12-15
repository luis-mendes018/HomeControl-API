using Application.DTOs.Usuario;

namespace Application.DTOs.Transacao;

public class TransacaoResponseDto
{
    public Guid Id { get; set; }
    public decimal Valor { get; set; }
    public string Descricao { get; set; }
    public Guid CategoriaId { get; set; }
    public Guid UsuarioId { get; set; }
    public UsuarioResponseDto Usuario { get; set; } 
}
