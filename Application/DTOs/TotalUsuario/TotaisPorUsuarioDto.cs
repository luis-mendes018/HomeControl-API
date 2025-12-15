using System.Text.Json.Serialization;

namespace Application.DTOs.TotalUsuario;

public class TotaisPorUsuarioDto
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Guid? UsuarioId { get; set; }
    public string Nome { get; set; }
    public decimal TotalReceita { get; set; }
    public decimal TotalDespesa { get; set; }
    public decimal Saldo => TotalReceita - TotalDespesa;
}
