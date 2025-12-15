namespace Application.DTOs.Transacao;

public class TransacaoCreateDto
{
    public decimal Valor { get; set; }
    public string Descricao { get; set; }
    public Guid CategoriaId { get; set; }

}