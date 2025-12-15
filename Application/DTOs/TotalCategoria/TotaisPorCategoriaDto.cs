using System.Text.Json.Serialization;

namespace Application.DTOs.TotalCategoria;

public class TotaisPorCategoriaDto
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Guid? CategoriaId { get; set; }
    public string Nome { get; set; }
    public decimal TotalReceita { get; set; }
    public decimal TotalDespesa { get; set; }
    public decimal Saldo => TotalReceita - TotalDespesa;
}
