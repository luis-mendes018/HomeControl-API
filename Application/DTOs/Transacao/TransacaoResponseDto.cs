using Application.DTOs.Categoria;
using Application.DTOs.Usuario;

using System.Text.Json.Serialization;

namespace Application.DTOs.Transacao;

public class TransacaoResponseDto
{
    public Guid Id { get; set; }
    public int CodigoTransacao { get; set; }
    public decimal Valor { get; set; }
    public string Descricao { get; set; }
    [JsonIgnore]
    public DateTime DataCriacao { get; set; }
    public string DataCriacaoDto => DataCriacao.ToString("dd/MM/yyyy HH:mm");
    public Guid CategoriaId { get; set; }
    public CategoriaResponseDto Categoria { get; set; }
    public Guid UsuarioId { get; set; }
    public UsuarioResponseDto Usuario { get; set; } 
}
