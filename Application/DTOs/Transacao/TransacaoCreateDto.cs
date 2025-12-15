using Application.DTOs.Usuario;

using Domain.Enums;

namespace Application.DTOs.Transacao;

public class TransacaoCreateDto
{
    public decimal Valor { get; set; }
    public string Descricao { get; set; }
    public Guid CategoriaId { get; set; }
    public TipoTransacao Tipo { get; set; }
    public Guid UsuarioId { get; set; }

}